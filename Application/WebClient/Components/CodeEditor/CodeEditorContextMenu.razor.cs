using Blazor.Heroicons;
using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public sealed partial class CodeEditorContextMenu: ComponentBase
{
    
    # region Parameters
    
    [Parameter, EditorRequired] public bool IsReadOnly { get; set; }
    [Parameter, EditorRequired] public bool IsOwner { get; set; }
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    
    # endregion
    
    # region Variables
    
    private EditFileNameModal EditModal { get; set; } = null!;
    private DeleteFileModal DeleteModal { get; set; } = null!;
    private List<ContextMenuEntry> ContextMenuEntries { get; set; } = new();
    
    # endregion

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;
        InitializeContextMenu();
        StateHasChanged();
    }

    private void InitializeContextMenu()
    {
        ContextMenuEntries = new List<ContextMenuEntry>
        {
            new("Edit name", HeroiconName.Pencil, EditModal, !IsReadOnly),
            new("Delete file", HeroiconName.Trash, DeleteModal, !IsReadOnly),
            new("Clone Project", HeroiconName.DocumentDuplicate, DeleteModal, !IsOwner)
        };
    }
    
    private static async Task CallChildFunction(IModalInvoke modal) => await modal.InvokeChildFunction();
}

internal class ContextMenuEntry
{
    public string Text { get; }
    public string IconName { get; }
    public IModalInvoke ModalTarget { get; }
    public bool IsVisible { get; }
    
    public ContextMenuEntry(string text, string iconName, IModalInvoke modalTarget, bool isVisible)
    {
        Text = text;
        IconName = iconName;
        ModalTarget = modalTarget;
        IsVisible = isVisible;
    }
}