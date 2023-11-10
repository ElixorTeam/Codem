using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Components.CodeEditor;

public sealed partial class EditFileNameModal: ComponentBase
{
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    
    [Parameter, EditorRequired] public Action<string> ChangeFileNameAction { get; set; } = null!;
    private IJSObjectReference Module { get; set; } = null!;
    
    private string _fileNamePattern = @"^[\w\-.]+(\.\w+)$";
    private bool IsErrorInput { get; set; } = false; 
    private string InputFileName { get; set; } = string.Empty;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./Components/CodeEditor/EditFileNameModal.razor.js");
        await Module.InvokeVoidAsync("initFileNameModal");
    }

    public async void InvokeChildFunction()
    {
        await ToggleDropdown();
    }
    
    private async void ChangeFileName()
    {
        if (!CheckIsCorrectFileName(InputFileName))
        {
            IsErrorInput = true;
            ToastService.ShowError("File name is incorrect");
            return;
        }
        
        ChangeFileNameAction(InputFileName);
        await ToggleDropdown();
    }
    
    private async Task ToggleDropdown()
    {
        await Module.InvokeVoidAsync("toggleFileNameModal");
        InputFileName = string.Empty;
        IsErrorInput = false;
    }

    private bool CheckIsCorrectFileName(string fileName) => Regex.IsMatch(fileName, _fileNamePattern);
}