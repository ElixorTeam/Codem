import { basicSetup } from "codemirror"
import { EditorView, keymap } from "@codemirror/view"
import { Compartment, EditorState } from "@codemirror/state"
import { indentWithTab } from "@codemirror/commands"
import { languages } from "@codemirror/language-data";
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

const getLangLib = async(name: string) => {
    try {
        return await languages.find(l => l.name == name).load()
    }
    catch {
        return await languages.find(l => l.name == "Markdown").load()
    }
}

export async function initCodeMirror(
    dotnetHelper: DotNetHelperType,
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
            languageComp.of(langLib),
            tabSizeComp.of(EditorState.tabSize.of(tabSize)),
            readOnlyComp.of(EditorState.readOnly.of(readOnly)),
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
        languageComp,
        tabSizeComp,
        readOnlyComp
    );
}

export const setText = (id: string, text: string) => {
    const transaction = CMInstances[id].getState().update({
        changes: {from: 0, to: CMInstances[id].getState().doc.length, insert: text}
    })
    CMInstances[id].view.dispatch(transaction)
}

export const setLanguage = async(id: string, lang: string) => {
    const langLib = await getLangLib(lang)
    const config = {
        effects: CMInstances[id].language.reconfigure(langLib)
    }
    CMInstances[id].view.dispatch(config)
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