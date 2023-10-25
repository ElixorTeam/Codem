import { autocompletion, acceptCompletion, currentCompletions } from '@codemirror/autocomplete'
import { indentMore, indentLess } from '@codemirror/commands'
import { markdown } from '@codemirror/lang-markdown'
import { languages } from '@codemirror/language-data'
import { Compartment, EditorState } from '@codemirror/state'
import { EditorView, keymap } from '@codemirror/view'
import { basicSetup } from 'codemirror'

import CodeMirrorInstance from './CodeMirrorInstance'
import { DotNetHelperType } from './DotNetHelperType'

const codeMirrorInstance: Map<string, CodeMirrorInstance> = new Map()

const createUpdateListener = (dotNetHelper: DotNetHelperType) => {
  const invokeMethodAsync = async (method: string, arg: unknown) => {
    await dotNetHelper.invokeMethodAsync(method, arg)
  }

  return EditorView.updateListener.of(async (update) => {
    const { docChanged, focusChanged, view, state } = update
    if (docChanged || (focusChanged && !view.hasFocus)) {
      const docString = state.doc.toString()
      await invokeMethodAsync('OnJsTextChanged', docString)
    }

    if (focusChanged) {
      const { hasFocus } = view
      await invokeMethodAsync('OnJsFocusChanged', hasFocus)
    }
  })
}

const getLangLib = async (name: string) => {
  try {
    const language = languages.find((l) => l.name === name)
    return language ? await language.load() : null
  } catch {
    return null
  }
}

export async function initCodeMirror(
  dotNetHelper: DotNetHelperType,
  id: string,
  initialText: string,
  readOnly: boolean,
  tabSize: number,
  language: string
) {
  const languageComp = new Compartment()
  const tabSizeComp = new Compartment()
  const readOnlyComp = new Compartment()

  const langLib = await getLangLib(language)

  const state = EditorState.create({
    doc: initialText,
    extensions: [
      basicSetup,
      languageComp.of(langLib ?? markdown()),
      tabSizeComp.of(EditorState.tabSize.of(tabSize)),
      readOnlyComp.of(EditorState.readOnly.of(readOnly)),
      keymap.of([
        {
          key: 'Tab',
          run: (view) => (currentCompletions(view.state).length ? acceptCompletion(view) : indentMore(view)),
          shift: indentLess,
          preventDefault: true,
        },
      ]),
      createUpdateListener(dotNetHelper),
      autocompletion(),
    ],
  })

  const parentElement = document.getElementById(id)
  if (!parentElement) {
    console.error(`Element with id '${id}' not found.`)
    return
  }

  const view = new EditorView({
    state,
    parent: parentElement,
  })

  const instance = new CodeMirrorInstance(dotNetHelper, view, {
    lang: languageComp,
    tabSize: tabSizeComp,
    readOnly: readOnlyComp,
  })
  codeMirrorInstance.set(id, instance)
}

export const setText = (id: string, text: string) => {
  const instance = codeMirrorInstance.get(id)
  if (!instance) {
    console.error(`CodeMirrorInstance with id '${id}' not found.`)
    return
  }

  const transaction = instance.getState().update({
    changes: {
      from: 0,
      to: instance.getState().doc.length,
      insert: text,
    },
  })
  instance.view.dispatch(transaction)
}

export const setLanguage = async (id: string, lang: string) => {
  const instance = codeMirrorInstance.get(id)
  if (!instance) {
    console.error(`CodeMirrorInstance with id '${id}' not found.`)
    return
  }

  const langLib = await getLangLib(lang)
  if (!langLib) {
    console.error(`The ${lang} is not in the database.`)
    return
  }

  const config = {
    effects: instance.ext.lang.reconfigure(langLib),
  }
  instance.view.dispatch(config)
}

export const setTabSize = (id: string, size: number) => {
  const instance = codeMirrorInstance.get(id)
  if (!instance) {
    console.error(`CodeMirrorInstance with id '${id}' not found.`)
    return
  }

  const config = {
    effects: instance.ext.tabSize.reconfigure(EditorState.tabSize.of(size)),
  }
  instance.view.dispatch(config)
}

export const setReadOnly = (id: string, value: boolean) => {
  const instance = codeMirrorInstance.get(id)
  if (!instance) {
    console.error(`CodeMirrorInstance with id '${id}' not found.`)
    return
  }

  const config = {
    effects: instance.ext.readOnly.reconfigure(EditorState.readOnly.of(value)),
  }
  instance.view.dispatch(config)
}

export const dispose = (id: string) => {
  if (!codeMirrorInstance.get(id)) {
    console.error(`CodeMirrorInstance with id '${id}' not found.`)
    return
  }

  codeMirrorInstance.delete(id)
}
