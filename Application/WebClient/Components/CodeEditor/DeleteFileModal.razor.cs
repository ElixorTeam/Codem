using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public sealed partial class DeleteFileModal: ComponentBase
{
    [Parameter, EditorRequired] public Action DeleteFileAction { get; set; } = null!;
}