namespace WebClient.Components.CodeEditor;

public class ContextMenuModel
{
    public string Text { get; init; } = string.Empty;
    public string IconName { get; init; } = string.Empty;
    public IModalInvoke ModalTarget { get; init; } = null!;
    public bool IsVisible { get; init; } = true;
}