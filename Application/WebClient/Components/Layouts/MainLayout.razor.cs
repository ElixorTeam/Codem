using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedCore.Enums;

namespace WebClient.Components.Layouts;

public sealed partial class MainLayout : LayoutComponentBase
{
    private ThemesEnum _currentTheme;
    
    [Inject] private IJSRuntime JSRuntime { get; set; }
    [Inject] public ILocalStorageService LocalStorage { get; set; }
    [Parameter] public ThemesEnum CurrentTheme
    {
        get => _currentTheme;
        set
        {
            _currentTheme = value;
            UpdateLocalStorage();
        }
    }
    
    private string ThemeName => CurrentTheme.ToString().ToLower();
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("initFlowbite");
            await InitializeTheme();
            StateHasChanged();
        }
    }
    
    private async Task InitializeTheme()
    {
        string theme;
        if (await LocalStorage.ContainKeyAsync("theme")) 
            theme = await LocalStorage.GetItemAsStringAsync("theme");
        else
            theme = await JSRuntime.InvokeAsync<string>("getPreferredTheme");
        CurrentTheme = (ThemesEnum)Enum.Parse(typeof(ThemesEnum), theme, true);
    }

    private void UpdateLocalStorage()
    {
        LocalStorage.SetItemAsStringAsync("theme",   ThemeName);
    }
}