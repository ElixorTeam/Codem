using Microsoft.AspNetCore.Components;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Enums;

namespace WebClient.Components;

public sealed partial class SearchItem : ComponentBase
{
    [Parameter, EditorRequired] public CodeSnippetModel SnippetModel { get; set; } = null!;
    
    private List<ProgrammingLanguage> GetMostUsedLanguages() =>
        LangUsageAnalyzer.GetMostUsedLanguages(SnippetModel.Files, 2);
}