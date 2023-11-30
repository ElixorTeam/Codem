using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public sealed partial class CodeFileTabs: ComponentBase
{
    [Inject] private IToastService ToastService { get; set; } = null!;
    
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    [Parameter] public bool IsReadOnly { get; set; }

    private void AddNewFile()
    {
        try
        {
            CodeFileManager.AddFile();
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }
}