using Blazor.Heroicons;
using Microsoft.AspNetCore.Components;
using Сodem.Shared.Enums;

namespace WebClient.Components.Layouts;

public sealed partial class MainHeader: ComponentBase
{
    [Parameter, EditorRequired] public ThemesEnum Theme { get; set; }
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