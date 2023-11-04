namespace WebClient.Components.CodeEditor;

public class ContextMenuModel
{
    public string Text { get; init; } = string.Empty;
    public string IconName { get; init; } = string.Empty;
    public string ModalTarget { get; init; } = string.Empty;
    public bool IsVisible { get; init; }
}