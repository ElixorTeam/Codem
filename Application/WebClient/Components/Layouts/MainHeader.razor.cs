using Blazor.Heroicons;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebClient.Common;
using Сodem.Shared.Enums;
using Сodem.Shared.Models;

namespace WebClient.Components.Layouts;

public sealed partial class MainHeader: ComponentBase
{
    [Inject] private IUserService UserService { get; set; } = null!;
    [Parameter, EditorRequired] public ThemesEnum Theme { get; set; }
    [Parameter] public EventCallback<ThemesEnum> ThemeChanged { get; set; }
    
    private UserModel? User { get; set; }

    protected override void OnInitialized()
    {
        User = UserService.GetUser();
    }

    private async Task ToggleTheme()
    {
        Array values = Enum.GetValues(typeof(ThemesEnum));
        int index = Array.IndexOf(values, Theme);
        int nextIndex = (index + 1) % values.Length;
        Theme = (ThemesEnum)(values.GetValue(nextIndex) ?? ThemesEnum.Dark);
        await ThemeChanged.InvokeAsync(Theme);
    }
}