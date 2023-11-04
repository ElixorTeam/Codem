using Blazor.Heroicons;

namespace WebClient.Components.Profile;

public partial class AccountInfo
{
    public List<StatModel> statList { get; set; } = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        statList = new List<StatModel>()
        {
            new() { IconName = @HeroiconName.CodeBracket, Value = "Python, C#, Java" },
            new() { IconName = @HeroiconName.Eye, Value = "1.4k" },
            new() { IconName = @HeroiconName.Star, Value = "120" },
        };
    }
}