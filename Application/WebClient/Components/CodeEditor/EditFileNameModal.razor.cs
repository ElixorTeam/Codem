using System.Text.RegularExpressions;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace WebClient.Components.CodeEditor;

public sealed partial class EditFileNameModal: ComponentBase, IModalInvoke, IDisposable
{
    
    # region Injects
    
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    
    # endregion
    
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    
    # region Variables
    
    private IJSObjectReference Module { get; set; } = null!;
    private bool IsErrorInput { get; set; } = false;
    private string InputFileName { get; set; } = string.Empty;
    private string ModalUniqueId { get; init; } = Guid.NewGuid().ToString();
    private string FileNamePattern { get; init; } = @"^(?=.{1,48}$)(\.?[\w\-.]+)(\.\w+)?$";
    
    # endregion
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        CodeFileManager.OnFileChange += UpdateFileNameOnCurrentFileChange;
        UpdateFileNameOnCurrentFileChange();
        await InitializeModal();
    }

    public async Task InvokeChildFunction() => await ToggleDropdown();
    
    private async Task InitializeModal()
    {
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/modalInterface.js");
        await Module.InvokeVoidAsync("initModal", "editFileNameModal", ModalUniqueId);
    }
    
    private void HandleChangeFileNameByEnter(KeyboardEventArgs e)
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
        
        ApplyNewFileName(processedFileName);
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
        await Module.InvokeVoidAsync("toggleModal", ModalUniqueId);
        InputFileName = CodeFileManager.GetCurrentFile().Title;
        IsErrorInput = false;
    }
    
    private void ApplyNewFileName(string fileName) =>
        CodeFileManager.ChangeFileName(CodeFileManager.GetCurrentFile().Id, fileName);

    private bool CheckIsCorrectFileName(string fileName) => 
        Regex.IsMatch(fileName, FileNamePattern);
    
    public void Dispose() => CodeFileManager.OnFileChange -= UpdateFileNameOnCurrentFileChange;
}