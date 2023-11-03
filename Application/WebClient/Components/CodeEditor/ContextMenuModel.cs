namespace WebClient.Components.CodeEditor;

public class ContextMenuModel
{
    public string Text { get; }
    public string Icon { get; }
    public string ModalTarget { get; }
    public bool IsVisible { get; }

    public ContextMenuModel(string text, string icon, string modalTarget, bool isVisible)
    {
        Text = text;
        Icon = icon;
        ModalTarget = modalTarget;
        IsVisible = isVisible;
    }
}