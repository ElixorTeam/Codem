import {basicSetup} from "codemirror"
import {EditorView, keymap} from "@codemirror/view"
import {Compartment, EditorState} from "@codemirror/state"
import {markdown, markdownLanguage} from "@codemirror/lang-markdown"
import {indentWithTab} from "@codemirror/commands"
import {languages} from "@codemirror/language-data"
import {autocompletion} from "@codemirror/autocomplete"
import {CmInstance} from "./CmInstance"

const CMInstances: { [id: string]: CmInstance } = {}

const createUpdateListener = (dotnetHelper: any) => {
    const invokeMethodAsync = async (method: string, arg: any) => {
        await dotnetHelper.invokeMethodAsync(method, arg);
    }

    return EditorView.updateListener.of(async (update) => {
        const { docChanged, focusChanged, view, state} = update;

        if (docChanged || (focusChanged && !view.hasFocus)) {
            const docString = state.doc.toString()
            await invokeMethodAsync("OnJsTextChanged", docString);
        }

        if (focusChanged) {
            const hasFocus = view.hasFocus;
            await invokeMethodAsync("OnJsFocusChanged", hasFocus);
        }
    });
}

export function initCodeMirror(dotnetHelper: any, id: string, initialText: string, readOnly: boolean) {
    const language = new Compartment()
    const tabSize = new Compartment()
    
    const state = EditorState.create({
        doc: initialText,
        extensions: [
            basicSetup,
            language.of(markdown({ base: markdownLanguage, codeLanguages: languages })),
            tabSize.of(EditorState.tabSize.of(4)),
            EditorState.readOnly.of(readOnly),
            keymap.of([indentWithTab]),
            createUpdateListener(dotnetHelper),
            autocompletion(),
        ]
    })
    
    const view = new EditorView({
        state,
        parent: document.getElementById(id),
    })

    CMInstances[id] = new CmInstance(dotnetHelper, view, language);
}

export const setText = (id: string, text: string) => {
    const transaction = CMInstances[id].getState().update({
        changes: {from: 0, to: CMInstances[id].getState().doc.length, insert: text}
    })
    CMInstances[id].view.dispatch(transaction)
}

export const setTabSize = (id: string, size: number) => {
}

export const setReadOnly = (id: string, value: boolean) => {
}

export const dispose = (id: string) => delete CMInstances[id]