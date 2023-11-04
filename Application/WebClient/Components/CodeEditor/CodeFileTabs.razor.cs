using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public partial class CodeFileTabs
{
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    [Parameter] public bool IsReadOnly { get; set; }
}