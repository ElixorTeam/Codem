using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public partial class ContextMenuEntry
{
    [Parameter, EditorRequired] public ContextMenuModel Entry { get; set; } = null!;
}