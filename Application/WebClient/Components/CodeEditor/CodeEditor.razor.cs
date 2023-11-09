using Microsoft.AspNetCore.Components;
using WebClient.Models;

namespace WebClient.Components.CodeEditor;

public sealed partial class CodeEditor : ComponentBase
{

    #region Parameters

    [Parameter, EditorRequired] public string ActiveLanguage { get; set; } = string.Empty;
    [Parameter] public CodeFileManager CodeFileManager { get; set; } = new();
    [Parameter] public List<CodeFileModel> CodeFileList { get; set; } = new();
    [Parameter] public bool IsReadOnly { get; set; }
    [Parameter] public bool IsOwner { get; set; }

    #endregion
    
    private CodeFileModel CurrentCodeFile { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        InitFileManager();
    }

    private void InitFileManager()
    {
        CodeFileManager = new CodeFileManager(CodeFileList);
        CodeFileManager.OnFileChange = () =>
        {
            CodeFileModel file = CodeFileManager.GetCurrentFile();
            CurrentCodeFile = file;
            ActiveLanguage = file.Language;
            StateHasChanged();
        };
        CurrentCodeFile = CodeFileManager.GetCurrentFile();
        ActiveLanguage = CurrentCodeFile.Language;
    }
    
    private void HandleActiveLanguageChanged(string language)
    {
        CodeFileManager.ChangeLanguageOfCurrentFile(language);
        ActiveLanguage = language;
    }
}