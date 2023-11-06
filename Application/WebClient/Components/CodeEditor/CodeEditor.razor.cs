using Microsoft.AspNetCore.Components;
using WebClient.Models;

namespace WebClient.Components.CodeEditor;

public sealed partial class CodeEditor : ComponentBase
{

    #region Parameters

    [Parameter, EditorRequired] public string ActiveLanguage { get; set; } = "Markdown";
    [Parameter] public bool IsReadOnly { get; set; }
    [Parameter] public bool IsOwner { get; set; }

    #endregion

    public CodeFileManager CodeFileManager { get; } = new();
    private CodeFileModel CodeFile { get; set; } = null!;
    
    private void HandleActiveLanguageChanged(string language)
    {
        ActiveLanguage = language;
        CodeFileManager.ChangeCurrentLanguage(language);
    }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
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