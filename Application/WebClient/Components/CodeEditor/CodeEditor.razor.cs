using Microsoft.AspNetCore.Components;
using WebClient.Models;

namespace WebClient.Components.CodeEditor;

public partial class CodeEditor : ComponentBase
{

    #region Parameters

    [Parameter] public string ActiveLanguage { get; set; } = "Markdown";
    [Parameter] public bool IsReadOnly { get; set; }
    [Parameter] public bool IsOwner { get; set; }

    #endregion

    public CodeFileManager CodeFileManager { get; } = new();
    private string InputFileName { get; set; } = string.Empty;
    private CodeFileModel CodeFile { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        CodeFileManager.OnFileChange = () => { CodeFile = CodeFileManager.GetCurrentFile(); StateHasChanged(); };
        CodeFile = CodeFileManager.GetCurrentFile();
    }
}