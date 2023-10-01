import { EditorView } from "@codemirror/view"
import {Compartment, EditorState} from "@codemirror/state"

export class CmInstance {
    public dotNetHelper: any
    public language: Compartment = new Compartment
    public view: EditorView
    
    constructor(dotNetHelper: any, view: EditorView, language: Compartment) {
        this.dotNetHelper = dotNetHelper;
        this.language = language;
        this.view = view;
    }
    
    public getState(): EditorState {
        return this.view.state;
    }
}
