using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedCore.Enums;

namespace WebClient.Components.Layouts;

public sealed partial class MainLayout : LayoutComponentBase
{
    # region Inject
    
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    [Inject] public ILocalStorageService LocalStorage { get; set; } = null!;
    
    # endregion
    [Parameter] public ThemesEnum CurrentTheme
    {
        get => _currentTheme;
        set
        {
            if (_currentTheme == value) return;
            _currentTheme = value;
            OnCurrentThemeChanged();
        }
    }
    
    private ThemesEnum _currentTheme;
    private IJSObjectReference Module { get; set; } = null!;
    
    private void OnCurrentThemeChanged()
    {
        SetThemeInHtml();
        UpdateLocalStorage();
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await JSRuntime.InvokeVoidAsync("initFlowbite");
        if (!firstRender) return;
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/themeUtils.js");
        await InitializeTheme();
        StateHasChanged();
    }
    
    private async Task InitializeTheme()
    {
        string theme = await LocalStorage.ContainKeyAsync("theme") ?
            await LocalStorage.GetItemAsStringAsync("theme") :
            await Module.InvokeAsync<string>("getPreferredTheme");
        CurrentTheme = (ThemesEnum)Enum.Parse(typeof(ThemesEnum), theme, true);
    }
    
    private string ThemeName => CurrentTheme.ToString().ToLower();

    private void SetThemeInHtml() => Module.InvokeVoidAsync("switchTheme", ThemeName);

    private void UpdateLocalStorage() => LocalStorage.SetItemAsStringAsync("theme", ThemeName);
}