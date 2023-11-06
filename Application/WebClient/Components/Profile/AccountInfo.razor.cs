using Blazor.Heroicons;
using Microsoft.AspNetCore.Components;

namespace WebClient.Components.Profile;

public sealed partial class AccountInfo: ComponentBase
{
    private List<StatModel> StatList { get; set; } = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        StatList = new List<StatModel>()
        {
            new() { IconName = @HeroiconName.CodeBracket, Value = "Python, C#, Java" },
            new() { IconName = @HeroiconName.Eye, Value = "1.4k" },
            new() { IconName = @HeroiconName.Star, Value = "120" },
        };
    }
}