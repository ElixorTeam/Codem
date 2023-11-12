using Microsoft.AspNetCore.Components;
using WebClient.Components.CodeEditor;

namespace WebClient.Pages;

public sealed partial class Index : ComponentBase
{
    private CodeFileManager CodeFileManager { get; set; } = new();
    private string ActivateLanguage { get; set; } = "Markdown";
}