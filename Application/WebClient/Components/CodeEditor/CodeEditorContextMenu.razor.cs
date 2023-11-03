using Blazor.Heroicons;
using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public partial class CodeEditorContextMenu
{
    [Parameter, EditorRequired] public bool IsReadOnly { get; set; }
    [Parameter, EditorRequired] public bool IsOwner { get; set; }
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;

    private List<ContextMenuModel> ContextMenuEntries = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        ContextMenuEntries = new List<ContextMenuModel>
        {
            new("Delete file", @HeroiconName.Trash, "deleteFileModal", !IsReadOnly),
            new("Rename file", @HeroiconName.Pencil, "editFileNameModal", !IsReadOnly),
            new("Clone Project", @HeroiconName.DocumentDuplicate, "editFileNameModal", !IsOwner),
            new("Remove Project", @HeroiconName.Trash, "editFileNameModal", IsOwner)
        };
    }

    public void ChangeFileName(String fileName)
    {
        CodeFileManager.ChangeFileName(CodeFileManager.GetCurrentFile().Id, fileName);
    }
}