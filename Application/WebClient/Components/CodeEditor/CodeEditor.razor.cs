using Microsoft.AspNetCore.Components;
using WebClient.Models;

namespace WebClient.Components.CodeEditor;

public partial class CodeEditor : ComponentBase
{

    #region Parameters

    [Parameter, EditorRequired] public string ActiveLanguage { get; set; } = null!;
    [Parameter] public bool IsReadOnly { get; set; }
    [Parameter] public bool IsOwner { get; set; }

    #endregion

    public CodeFileManager CodeFileManager { get; } = new();
    private CodeFileModel CodeFile { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        CodeFileManager.OnFileChange = () => { CodeFile = CodeFileManager.GetCurrentFile(); StateHasChanged(); };
        CodeFile = CodeFileManager.GetCurrentFile();
    }
}