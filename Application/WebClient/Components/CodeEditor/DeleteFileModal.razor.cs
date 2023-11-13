using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Components.CodeEditor;

public sealed partial class DeleteFileModal: ComponentBase, IModalInvoke
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    
    private IJSObjectReference Module { get; set; } = null!;
    private string ModalUniqueId { get; init; } = Guid.NewGuid().ToString();
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await InitializeModal();
    }
    
    public async Task InvokeChildFunction() => await ToggleDropdown();

    private async Task InitializeModal()
    {
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/modalInterface.js");
        await Module.InvokeVoidAsync("initModal", "deleteFileModal", ModalUniqueId);
    }
    
    private async Task ToggleDropdown() => 
        await Module.InvokeVoidAsync("toggleModal", ModalUniqueId);

    private async Task DeleteCurrentFile()
    {
        CodeFileManager.DeleteFile(CodeFileManager.GetCurrentFile().Id);
        await ToggleDropdown();
    }
}