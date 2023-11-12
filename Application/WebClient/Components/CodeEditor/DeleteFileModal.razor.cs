using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Components.CodeEditor;

public sealed partial class DeleteFileModal: ComponentBase, IModalInvoke
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    
    private IJSObjectReference Module { get; set; } = null!;
    private string _modalUniqueId = Guid.NewGuid().ToString();
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/modalInterface.js");
        await Module.InvokeVoidAsync("initModal", "deleteFileModal", _modalUniqueId);
    }
    
    public async void InvokeChildFunction() => await ToggleDropdown();
    
    private async Task ToggleDropdown()
    {
        await Module.InvokeVoidAsync("toggleModal", _modalUniqueId);
    }

    private async void DeleteFile()
    {
        CodeFileManager.DeleteFile(CodeFileManager.GetCurrentFile().Id);
        await ToggleDropdown();
    }
}