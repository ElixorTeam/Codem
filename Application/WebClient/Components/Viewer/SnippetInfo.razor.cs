using Blazor.Heroicons;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Enums;

namespace WebClient.Components.Viewer;

public sealed partial class SnippetInfo : ComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Parameter, EditorRequired] public CodeSnippetModel SnippetModel { get; set; } = new();
    private List<SnippetStatModel> StatsList { get; set; } = new();
    private string SnippetLink { get; set; } = string.Empty;

    private void OnCopyToClipboard()
    {
        try
        {
            CopyLinkToClipboard();
            ToastService.ShowSuccess("Link added to a clipboard");
            
        }
        catch (Exception)
        {
            ToastService.ShowError("Error while adding to a clipboard");
        }
    }
    
    private ValueTask CopyLinkToClipboard() =>
        JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", SnippetLink);

    private string GetMostUsedLanguages()
    {
        List<ProgrammingLanguage> mostUsedLanguages = LangUsageAnalyzer.GetMostUsedLanguages(SnippetModel.Files, 2);
        return string.Join(", ", mostUsedLanguages.Select(language => EnumHelper.GetEnumDescription(language)));
    }
    
    protected override void OnInitialized()
    {
        SnippetLink = $"https://localhost:5001{RouteUtils.Snippet}/{SnippetModel.Id}";
        StatsList = new()
        {
            // new() { IconName = HeroiconName.User, Value = Author },
            new() { IconName = HeroiconName.CalendarDays, Value = SnippetModel.CreateDate.ToString() },
            new() { IconName = HeroiconName.CodeBracket, Value = GetMostUsedLanguages() },
            // new() { IconName = HeroiconName.Eye, Value = Views.ToString() }
            // new() { IconName = @HeroiconName.Star, Value = Stars.ToString() }
        };
    }
}

internal class SnippetStatModel
{
    public string IconName { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
}