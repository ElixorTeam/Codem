using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public partial class CodeFileTabs
{
    [Parameter] public bool IsReadOnly { get; set; }
    [Parameter] public CodeFileManager CodeFileManager { get; set; }
}