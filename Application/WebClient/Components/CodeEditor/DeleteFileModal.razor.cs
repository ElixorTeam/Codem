using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public partial class DeleteFileModal
{
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
}