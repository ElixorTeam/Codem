using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Components.CodeEditor;

public sealed partial class EditFileNameModal: ComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    private IJSObjectReference Module { get; set; } = null!;
    [Parameter, EditorRequired] public Action<string> ChangeFileNameAction { get; set; } = null!;
    
    private string InputFileName { get; set; } = string.Empty;

    public async void InvokeChildFunction()
    {
        await ToggleDropdown();
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./Components/CodeEditor/EditFileNameModal.razor.js");
        await Module.InvokeVoidAsync("initFileNameModal");
    }
    
    private async Task ToggleDropdown() => await Module.InvokeVoidAsync("toggleFileNameModal");

    private async void ChangeFileName()
    {
        ChangeFileNameAction(InputFileName);
        InputFileName = string.Empty;
        await ToggleDropdown();
    }
}