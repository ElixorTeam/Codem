using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public partial class DeleteFileModal
{
    [Parameter] public CodeFileManager CodeFileManager { get; set; }
}