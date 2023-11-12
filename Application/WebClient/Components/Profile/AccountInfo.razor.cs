using Blazor.Heroicons;
using Microsoft.AspNetCore.Components;
using WebClient.Common;
using Сodem.Shared.Models;

namespace WebClient.Components.Profile;

public sealed partial class AccountInfo: ComponentBase
{
    [Inject] private IUserService UserService { get; set; } = null!;
    
    private UserModel User { get; set; }
    private List<StatModel> StatList { get; set; } = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        User = UserService.GetUser() ?? new (string.Empty, string.Empty, string.Empty);
        StatList = new()
        {
            new() { IconName = @HeroiconName.CodeBracket, Value = "Python, C#, Java" },
            new() { IconName = @HeroiconName.Eye, Value = "1.4k" },
            new() { IconName = @HeroiconName.Star, Value = "120" },
        };
    }
}