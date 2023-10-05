import { EditorView } from "@codemirror/view"
import { Compartment, EditorState } from "@codemirror/state"
import { DotNetHelperType } from "./DotNetHelperType";

export class CodeMirrorInstance {
    constructor(
        public dotNetHelper: DotNetHelperType,
        public view: EditorView,
        public ext: {
          lang: Compartment,
          tabSize: Compartment,
          readOnly: Compartment, 
        }
    ) {}
    
    public getState(): EditorState {
        return this.view.state;
    }
}
