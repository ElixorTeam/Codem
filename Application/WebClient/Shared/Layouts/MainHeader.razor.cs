using Microsoft.AspNetCore.Components;
using SharedCore.Enums;

namespace WebClient.Shared.Layouts;

public partial class MainHeader
{
    [Parameter] public ThemesEnum Theme { get; set; }
    [Parameter] public EventCallback<ThemesEnum> ThemeChanged { get; set; }

    private async Task ToggleTheme()
    {
        Array values = Enum.GetValues(typeof(ThemesEnum));
        int index = Array.IndexOf(values, Theme);
        int nextIndex = (index + 1) % values.Length;
        Theme = (ThemesEnum)(values.GetValue(nextIndex) ?? ThemesEnum.Dark);
        await ThemeChanged.InvokeAsync(Theme);
    }
}