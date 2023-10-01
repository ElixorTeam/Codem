import { EditorView } from "@codemirror/view"
import {Compartment, EditorState} from "@codemirror/state"

export class CmInstance {
    public dotNetHelper: any
    public language: Compartment = new Compartment
    public tabSize: Compartment = new Compartment
    public readonly: Compartment = new Compartment
    public view: EditorView
    
    constructor(
        dotNetHelper: any,
        view: EditorView,
        language: Compartment,
        tabSize: Compartment,
        readonly: Compartment
    ) {
        this.dotNetHelper = dotNetHelper;
        this.view = view;
        this.language = language;
        this.tabSize = tabSize;
        this.readonly = readonly;
    }
    
    public getState(): EditorState {
        return this.view.state;
    }
}
