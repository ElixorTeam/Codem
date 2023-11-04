using Blazor.Heroicons;

namespace WebClient.Components.Profile;

public partial class AccountInfo
{
    public List<StatModel> StatList { get; set; } = new();
    
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