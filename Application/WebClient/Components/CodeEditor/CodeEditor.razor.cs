using Microsoft.AspNetCore.Components;
using WebClient.Models;

namespace WebClient.Components.CodeEditor;

public sealed partial class CodeEditor : ComponentBase
{

    #region Parameters

    [Parameter, EditorRequired] public string ActiveLanguage { get; set; } = "Markdown";
    [Parameter] public CodeFileManager CodeFileManager { get; set; } = new();
    [Parameter] public List<CodeFileModel> CodeFileList { get; set; } = new();
    [Parameter] public bool IsReadOnly { get; set; }
    [Parameter] public bool IsOwner { get; set; }

    #endregion
    private CodeFileModel CodeFile { get; set; } = null!;
    
    private void HandleActiveLanguageChanged(string language)
    {
        ActiveLanguage = language;
        CodeFileManager.ChangeCurrentLanguage(language);
    }
    
    protected override void OnInitialized()
    {
        CodeFileManager = new CodeFileManager(CodeFileList);
        CodeFileManager.OnFileChange = () =>
        {
            CodeFileModel file = CodeFileManager.GetCurrentFile();
            CodeFile = file;
            ActiveLanguage = file.Language;
            StateHasChanged();
        };
        CodeFile = CodeFileManager.GetCurrentFile();
    }
}