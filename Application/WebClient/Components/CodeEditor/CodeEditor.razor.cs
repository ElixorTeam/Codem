using Microsoft.AspNetCore.Components;
using WebClient.Models;
using Сodem.Shared.Enums;

namespace WebClient.Components.CodeEditor;

public sealed partial class CodeEditor : ComponentBase
{

    #region Parameters

    [Parameter, EditorRequired] public ProgrammingLanguage ActiveLanguage { get; set; } = ProgrammingLanguage.Markdown;
    [Parameter] public CodeFileManager CodeFileManager { get; set; } = new();
    [Parameter] public List<CodeFileModel> CodeFileList { get; set; } = new();
    [Parameter] public bool IsReadOnly { get; set; }
    [Parameter] public bool IsOwner { get; set; }

    #endregion
    
    private CodeFileModel CurrentCodeFileModel { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        InitFileManager();
    }

    private void InitFileManager()
    {
        CodeFileManager = new CodeFileManager(CodeFileList);
        CodeFileManager.OnFileChange += () =>
        {
            CodeFileModel fileModel = CodeFileManager.GetCurrentFile();
            CurrentCodeFileModel = fileModel;
            ActiveLanguage = fileModel.Language;
            StateHasChanged();
        };
        CurrentCodeFileModel = CodeFileManager.GetCurrentFile();
        ActiveLanguage = CurrentCodeFileModel.Language;
    }
    
    private void HandleActiveLanguageChanged(ProgrammingLanguage language)
    {
        CodeFileManager.ChangeLanguageOfCurrentFile(language);
        ActiveLanguage = language;
    }
}