using Blazor.Heroicons;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Components.Viewer;

public sealed partial class SnippetInfo : ComponentBase
{
    [Parameter] public string Author { get; set; } = string.Empty;
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public DateOnly CreateDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    // [Parameter] public string Description { get; set; } = string.Empty;
    [Parameter] public string ProgramLanguage { get; set; } = string.Empty;
    [Parameter] public int Views { get; set; } = int.MinValue;
    // [Parameter] public int Stars { get; set; } = int.MinValue;
    [Parameter] public string Access { get; set; } = string.Empty;

    private List<SnippetStatModel> StatsList { get; set; } = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        StatsList = new List<SnippetStatModel>()
        {
            new() { IconName = @HeroiconName.User, Value = Author },
            new() { IconName = @HeroiconName.CalendarDays, Value = CreateDate.ToString() },
            new() { IconName = @HeroiconName.CodeBracket, Value = ProgramLanguage },
            new() { IconName = @HeroiconName.Eye, Value = Views.ToString() }
            // new() { IconName = @HeroiconName.Star, Value = Stars.ToString() }
        };
    }
}

internal class SnippetStatModel
{
    public string IconName { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
}