using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public sealed partial class CodeFileTabs: ComponentBase
{
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    [Parameter] public bool IsReadOnly { get; set; }
}