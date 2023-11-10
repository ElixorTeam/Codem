using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace WebClient.Components.CodeEditor;

public sealed partial class EditFileNameModal: ComponentBase
{
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    private IJSObjectReference Module { get; set; } = null!;
    
    private string _fileNamePattern = @"^(?=.{1,48}$)(\.?[\w\-.]+)(\.\w+)?$";
    private bool IsErrorInput { get; set; } = false; 
    private string InputFileName { get; set; } = string.Empty;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        CodeFileManager.OnFileChange += UpdateFileNameOnCurrentFileChange;
        UpdateFileNameOnCurrentFileChange();
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./Components/CodeEditor/EditFileNameModal.razor.js");
        await Module.InvokeVoidAsync("initFileNameModal");
    }

    public async void InvokeChildFunction() => await ToggleDropdown();
    
    private void ChangeFileNameByEnter(KeyboardEventArgs e)
    {
        if (e.Code is "Enter" or "NumpadEnter")
            ChangeFileName();
    }

    private void UpdateFileNameOnCurrentFileChange()
    {
        InputFileName = CodeFileManager.GetCurrentFile().Title;
        StateHasChanged();
    }
    
    private async void ChangeFileName()
    {
        string processedFileName = ProcessFileName(InputFileName);
        if (!CheckIsCorrectFileName(processedFileName))
        {
            IsErrorInput = true;
            ToastService.ShowError("File name is incorrect");
            return;
        }
        
        CodeFileManager.ChangeFileName(CodeFileManager.GetCurrentFile().Id, processedFileName);
        await ToggleDropdown();
    }
    
    private static string ProcessFileName(string fileName)
    {
        fileName = fileName.Trim();
        if (fileName.EndsWith(".")) fileName = fileName[..^1];
        if (!fileName.Contains('.')) fileName += ".txt";
        return fileName;
    }

    private async Task ToggleDropdown()
    {
        await Module.InvokeVoidAsync("toggleFileNameModal");
        InputFileName = CodeFileManager.GetCurrentFile().Title;
        IsErrorInput = false;
    }

    private bool CheckIsCorrectFileName(string fileName) => Regex.IsMatch(fileName, _fileNamePattern);
    
    public void Dispose()
    {
        CodeFileManager.OnFileChange -= UpdateFileNameOnCurrentFileChange;
    }
}