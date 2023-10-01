using Microsoft.AspNetCore.Components;

namespace WebClient.Shared.Components;

public partial class CodeEditor : ComponentBase
{
    [Parameter] public string ActiveLanguage { get; set; }
    [Parameter] public bool IsReadOnly { get; set; }

    public CodeEditor()
    {
        IsReadOnly = false;
        ActiveLanguage = "Markdown";
    } 
}