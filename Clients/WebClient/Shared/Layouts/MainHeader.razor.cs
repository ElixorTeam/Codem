using Microsoft.AspNetCore.Components;

namespace WebClient.Shared.Layouts;

public partial class MainHeader
{
    [Parameter]
    public bool IsDarkMode { get; set; }

    [Parameter]
    public EventCallback<bool> IsDarkModeChanged { get; set; }

    private async Task ToggleTheme()
    {
        IsDarkMode = !IsDarkMode;
        await IsDarkModeChanged.InvokeAsync(IsDarkMode);
    }
}