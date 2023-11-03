using Microsoft.AspNetCore.Components;

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
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        CodeFileManager.OnFileChange = StateHasChanged;
    }
}