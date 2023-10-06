import { Compartment, EditorState } from '@codemirror/state'
import { EditorView } from '@codemirror/view'

import { DotNetHelperType } from './DotNetHelperType'

export default class CodeMirrorInstance {
  constructor(
    public dotNetHelper: DotNetHelperType,
    public view: EditorView,
    public ext: {
      lang: Compartment
      tabSize: Compartment
      readOnly: Compartment
    }
  ) {}

  public getState(): EditorState {
    return this.view.state
  }
}
