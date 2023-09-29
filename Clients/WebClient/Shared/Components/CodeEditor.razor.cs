using Microsoft.AspNetCore.Components;

namespace WebClient.Shared.Components;

public partial class CodeEditor : ComponentBase
{
    [Parameter] public string EditorCode { get; set; }

    [Parameter] public bool IsEditable { get; set; }

    public CodeEditor()
    {
        EditorCode = "";
        IsEditable = true;
    }
}