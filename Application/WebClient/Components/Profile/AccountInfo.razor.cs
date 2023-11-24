using Blazor.Heroicons;
using Microsoft.AspNetCore.Components;
using WebClient.Common;
using WebClient.Utils;
using Сodem.Shared.Models;

namespace WebClient.Components.Profile;

public sealed partial class AccountInfo: ComponentBase
{
    [Inject] private IUserService UserService { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    
    private UserModel? User { get; set; }
    private List<StatModel> StatList { get; set; } = new();
    
    protected override void OnInitialized()
    {
        User = UserService.GetUser();
        StatList = new List<StatModel>
        {
            new() { IconName = @HeroiconName.CodeBracket, Value = "Python, C#, Java" },
            new() { IconName = @HeroiconName.Eye, Value = "120" },
            // new() { IconName = @HeroiconName.Star, Value = "120" },
        };
    }
}

internal class StatModel
{
    public string IconName { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
}