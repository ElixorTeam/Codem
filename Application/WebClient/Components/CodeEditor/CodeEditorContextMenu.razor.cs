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
    
    private EditFileNameModal _editModal;
    private DeleteFileModal _deleteModal;
    
    private List<ContextMenuModel> _contextMenuEntries = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        _contextMenuEntries = new List<ContextMenuModel>
        {
            new()
            {
                Text="Edit name",
                IconName = @HeroiconName.Pencil,
                ModalTarget = _editModal,
                IsVisible = !IsReadOnly
            },
            new()
            {
                Text="Delete file",
                IconName = @HeroiconName.Trash,
                ModalTarget = _deleteModal,
                IsVisible = !IsReadOnly
            },
            new()
            {
                Text="Clone Project", 
                IconName = @HeroiconName.DocumentDuplicate,
                ModalTarget = _deleteModal,
                IsVisible = !IsOwner
            },
            new()
            {
                Text="Remove Project",
                IconName = @HeroiconName.Trash,
                ModalTarget = _deleteModal,
                IsVisible = IsOwner
            }
        };
        StateHasChanged();
    }
    
    private static void CallChildFunction(IModalInvoke modal) => modal.InvokeChildFunction();
    
}