using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public sealed partial class EditFileNameModal: ComponentBase
{
    [Parameter, EditorRequired] public Action<string> ChangeFileNameAction { get; set; } = null!;
    
    private string InputFileName { get; set; } = string.Empty;

    private void ChangeFileName()
    {
        ChangeFileNameAction(InputFileName);
        InputFileName = string.Empty;
    }
}