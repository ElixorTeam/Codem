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
    
    private EditFileNameModal editModal;
    
    private List<ContextMenuModel> _contextMenuEntries = new();
    
    public void ChangeFileName(string fileName) =>
        CodeFileManager.ChangeFileName(CodeFileManager.GetCurrentFile().Id, fileName);

    public void DeleteCurrentFile() => CodeFileManager.DeleteFile(CodeFileManager.GetCurrentFile().Id);
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        _contextMenuEntries = new List<ContextMenuModel>
        {
            new()
            {
                Text="Delete file",
                IconName = @HeroiconName.Trash,
                ModalTarget = "deleteFileModal",
                IsVisible = !IsReadOnly
            },
            // new()
            // {
            //     Text="Rename file",
            //     IconName = @HeroiconName.Pencil,
            //     ModalTarget = "editFileNameModal",
            //     IsVisible = !IsReadOnly
            // },
            new()
            {
                Text="Clone Project", 
                IconName = @HeroiconName.DocumentDuplicate,
                ModalTarget = "editFileNameModal",
                IsVisible = !IsOwner
            },
            new()
            {
                Text="Remove Project",
                IconName = @HeroiconName.Trash,
                ModalTarget = "editFileNameModal",
                IsVisible = IsOwner
            }
        };
    }
    
    private void CallChildFunction() => editModal.InvokeChildFunction();
}