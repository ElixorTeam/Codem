using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Сodem.Shared.Enums;

namespace WebClient.Components;

public sealed partial class ThemeChange
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    [Inject] public ILocalStorageService LocalStorage { get; set; } = null!;

    public ThemesEnum CurrentTheme { get; set; } = ThemesEnum.Light;
    private IJSObjectReference Module { get; set; } = null!;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/themeUtils.js");
        await InitializeTheme();
        await OnCurrentThemeChanged();
        StateHasChanged();
    }
    
    private async Task ToggleTheme()
    {
        SwitchTheme();
        await OnCurrentThemeChanged();
        StateHasChanged();
    }

    private void SwitchTheme()
    {
        Array values = Enum.GetValues(typeof(ThemesEnum));
        int index = Array.IndexOf(values, CurrentTheme);
        int nextIndex = (index + 1) % values.Length;
        CurrentTheme = (ThemesEnum)(values.GetValue(nextIndex) ?? ThemesEnum.Dark);
    }
    
    private async Task OnCurrentThemeChanged()
    {
        await SetThemeInHtml();
        await UpdateLocalStorage();
    }
    
    private async Task InitializeTheme()
    {
        string theme = await LocalStorage.ContainKeyAsync("theme") ?
            await LocalStorage.GetItemAsStringAsync("theme") :
            await Module.InvokeAsync<string>("getPreferredTheme");
        CurrentTheme = (ThemesEnum)Enum.Parse(typeof(ThemesEnum), theme, true);
    }
    
    private string ThemeName => CurrentTheme.ToString().ToLower();

    private async Task SetThemeInHtml() => await Module.InvokeVoidAsync("switchTheme", ThemeName);

    private async Task UpdateLocalStorage() => await LocalStorage.SetItemAsStringAsync("theme", ThemeName);
}
