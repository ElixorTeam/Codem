import { basicSetup } from "codemirror"
import { EditorView, keymap } from "@codemirror/view"
import { Compartment, EditorState } from "@codemirror/state"
import { markdown, markdownLanguage } from "@codemirror/lang-markdown"
import { indentWithTab } from "@codemirror/commands"
import { languages } from "@codemirror/language-data"
import { autocompletion } from "@codemirror/autocomplete"
import { CmInstance } from "./CmInstance"
import { DotNetHelperType } from "./DotNetHelperType";

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

export function initCodeMirror(
    dotnetHelper: DotNetHelperType,
    id: string,
    initialText: string,
    isReadOnly: boolean,
    customTabSize: number,
) {
    const language = new Compartment()
    const tabSize = new Compartment()
    const readOnly = new Compartment()
    
    const state = EditorState.create({
        doc: initialText,
        extensions: [
            basicSetup,
            language.of(markdown({ base: markdownLanguage, codeLanguages: languages })),
            tabSize.of(EditorState.tabSize.of(customTabSize)),
            readOnly.of(EditorState.readOnly.of(isReadOnly)),
            keymap.of([indentWithTab]),
            createUpdateListener(dotnetHelper),
            autocompletion(),
        ]
    })
    
    const view = new EditorView({
        state,
        parent: document.getElementById(id),
    })

    CMInstances[id] = new CmInstance(
        dotnetHelper,
        view,
        language,
        tabSize,
        readOnly
    );
}

export const setText = (id: string, text: string) => {
    const transaction = CMInstances[id].getState().update({
        changes: {from: 0, to: CMInstances[id].getState().doc.length, insert: text}
    })
    CMInstances[id].view.dispatch(transaction)
}

export const setTabSize = (id: string, size: number) => {
    const config = {
        effects: CMInstances[id].tabSize.reconfigure(EditorState.tabSize.of(size))
    }
    CMInstances[id].view.dispatch(config)
}

export const setReadOnly = (id: string, value: boolean) => {
    const config = {
        effects: CMInstances[id].readonly.reconfigure(EditorState.readOnly.of(value))
    }
    CMInstances[id].view.dispatch(config)
}

export const dispose = (id: string) => delete CMInstances[id]