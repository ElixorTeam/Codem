import { basicSetup } from "codemirror"
import { EditorView, keymap, placeholder } from "@codemirror/view"
import { EditorState, Compartment } from "@codemirror/state"
import { markdown, markdownLanguage } from "@codemirror/lang-markdown"
import { indentWithTab } from "@codemirror/commands"
import { languages } from "@codemirror/language-data"
import { autocompletion } from "@codemirror/autocomplete"
import { CmInstance } from "./CmInstance"

const CMInstances: { [id: string]: CmInstance } = {}


const createUpdateListener = (dotnetHelper: any) => {
    const invokeMethodAsync = async (method: string, arg: any) => {
        await dotnetHelper.invokeMethodAsync(method, arg);
    }

    return EditorView.updateListener.of(async (update) => {
        const { docChanged, focusChanged, view, state, selectionSet } = update;

        if (docChanged || (focusChanged && !view.hasFocus)) {
            const docString = state.doc.toString()
            await invokeMethodAsync("DocChanged", docString);
        }

        if (focusChanged) {
            const hasFocus = view.hasFocus;
            await invokeMethodAsync("FocusChanged", hasFocus);
        }

        if (selectionSet) {
            const ranges = state.selection.ranges.map(r => ({ from: r.from, to: r.to }));
            await invokeMethodAsync("SelectionSet", ranges);
        }
    });
}


export function initCodeMirror(
    dotnetHelper: any,
    id: string,
    initialText: string,
    placeholderText: string,
    tabulationSize: number
) {
    const language = new Compartment()
    const tabSize = new Compartment()
    const state = EditorState.create({
        doc: initialText,
        extensions: [
            basicSetup,
            language.of(markdown({ base: markdownLanguage, codeLanguages: languages })),
            tabSize.of(EditorState.tabSize.of(4)),
            keymap.of([indentWithTab]),
            createUpdateListener(dotnetHelper),
            autocompletion(),
        ]
    })
    
    const view = new EditorView({
        state,
        parent: document.getElementById(id),
    })

    const cmInstance = new CmInstance();
    cmInstance.dotNetHelper = dotnetHelper;
    cmInstance.state = state;
    cmInstance.view = view;
    cmInstance.tabSize = tabSize;
    cmInstance.language = language;
    
    CMInstances[id] = cmInstance;
}

export const setTabSize = (id: string, size: number) => {
    CMInstances[id].view.dispatch({
        effects: CMInstances[id].tabSize.reconfigure(EditorState.tabSize.of(size))
    })
}

export const setText = (id: string, text: string) => {
    const transaction = CMInstances[id].view.state.update({
        changes: {from: 0, to: CMInstances[id].view.state.doc.length, insert: text}
    })
    CMInstances[id].view.dispatch(transaction)
}

export const dispose = (id: string) => CMInstances[id]