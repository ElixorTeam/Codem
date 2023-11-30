using Microsoft.AspNetCore.Components;
using WebClient.Common;
using Сodem.Shared.Models;

namespace WebClient.Components.Layouts;

public sealed partial class MainHeader: ComponentBase
{
    [Inject] private IUserService UserService { get; set; } = null!;
    private UserModel? User { get; set; }

    protected override void OnInitialized()
    {
        User = UserService.GetUser();
    }
}