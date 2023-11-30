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
    # region Injects
    
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    
    # endregion
    
    [Parameter, EditorRequired] public CodeSnippetModel SnippetModel { get; set; } = new();
    [Parameter] public bool IsOwner { get; set; }
    private List<SnippetStatModel> StatsList { get; set; } = new();
    private string SnippetLink { get; set; } = string.Empty;
    
    protected override void OnInitialized()
    {
        SnippetLink = $"{NavigationManager.BaseUri[..^1]}{RouteUtils.Snippet}/{SnippetModel.Id}";
        StatsList = new()
        {
            // new() { IconName = HeroiconName.User, Value = Author },
            new() { IconName = HeroiconName.CalendarDays, Value = SnippetModel.CreateDate.ToString() },
            new() { IconName = HeroiconName.CodeBracket, Value = GetMostUsedLanguages() },
            // new() { IconName = HeroiconName.Eye, Value = Views.ToString() }
            // new() { IconName = HeroiconName.Star, Value = Stars.ToString() }
        };
    }

    private string GetEditSnippetLink() => $"{RouteUtils.Editor}/{SnippetModel.Id}";

    private async Task OnCopyToClipboard()
    {
        try
        {
            await CopyLinkToClipboard();
            ToastService.ShowSuccess("Link added to a clipboard");
            
        }
        catch (Exception)
        {
            ToastService.ShowError("Error while adding to a clipboard");
        }
    }
    
    private ValueTask CopyLinkToClipboard() =>
        JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", SnippetLink);

    private string GetMostUsedLanguages()
    {
        List<ProgrammingLanguage> mostUsedLanguages = LangUsageAnalyzer.GetMostUsedLanguages(SnippetModel.Files, 2);
        return string.Join(", ", mostUsedLanguages.Select(language => EnumHelper.GetEnumDescription(language)));
    }
}

internal class SnippetStatModel
{
    public string IconName { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
}