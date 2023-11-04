using Blazor.Heroicons;
using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public partial class CodeEditorContextMenu
{
    
    # region Parameters
    
    [Parameter, EditorRequired] public bool IsReadOnly { get; set; }
    [Parameter, EditorRequired] public bool IsOwner { get; set; }
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    
    # endregion

    private List<ContextMenuModel> ContextMenuEntries = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        ContextMenuEntries = new List<ContextMenuModel>
        {
            new()
            {
                Text="Delete file",
                IconName = @HeroiconName.Trash,
                ModalTarget = "deleteFileModal",
                IsVisible = !IsReadOnly
            },
            new()
            {
                Text="Rename file",
                IconName = @HeroiconName.Pencil,
                ModalTarget = "editFileNameModal",
                IsVisible = !IsReadOnly
            },
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

    public void ChangeFileName(string fileName)
    {
        CodeFileManager.ChangeFileName(CodeFileManager.GetCurrentFile().Id, fileName);
    }
}