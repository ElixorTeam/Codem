using System.Reflection;
using Blazor.Heroicons;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Components.CodeEditor;

public sealed partial class CodeEditorContextMenu: ComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    
    # region Parameters
    
    [Parameter, EditorRequired] public bool IsReadOnly { get; set; }
    [Parameter, EditorRequired] public bool IsOwner { get; set; }
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    
    # endregion
    
    # region Variables
    
    private EditFileNameModal EditModal { get; set; } = null!;
    private DeleteFileModal DeleteModal { get; set; } = null!;
    private List<ContextMenuEntry> ContextMenuEntries { get; set; } = new();
    private IJSObjectReference Module { get; set; } = null!;
    private string DropdownUniqueId { get; init; } = Guid.NewGuid().ToString();
    
    # endregion
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        InitializeContextMenu();
        await InitializeDropdown();
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

    private async Task InitializeDropdown()
    {
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/dropdownInterface.js");
        var options = new { offsetSkidding = -60 };
        await Module.InvokeVoidAsync("initDropdown", "editorContextMenu",
            "editorContextMenuButton", DropdownUniqueId, options);
    }
    
    private async Task HideDropdown() => 
        await Module.InvokeVoidAsync("hideDropdown", DropdownUniqueId);
    
    private async Task CallChildFunction(IModalInvoke modal)
    {
        await HideDropdown();
        await modal.InvokeChildFunction();
    }
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