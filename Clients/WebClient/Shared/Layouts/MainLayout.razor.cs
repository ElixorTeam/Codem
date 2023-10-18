using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Shared.Layouts;

public sealed partial class MainLayout : LayoutComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; }

    [Inject]
    public ILocalStorageService LocalStorage { get; set; }

    [Parameter]
    public bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            _isDarkMode = value;
            if (_localStorageHasBeenSet)
            {
                UpdateLocalStorage();
            }
            else
            {
                _localStorageHasBeenSet = true;
            }
        }
    }
    private bool _isDarkMode;
    private bool _localStorageHasBeenSet;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("initFlowbite");
            await InitializeTheme();
            StateHasChanged();
        }
    }

    async Task InitializeTheme()
    {
        if (await LocalStorage.ContainKeyAsync("theme"))
        {
            string theme = await LocalStorage.GetItemAsStringAsync("theme");
            IsDarkMode = theme == "dark";
            _localStorageHasBeenSet = true;
        }
        else
        {
            string preferredTheme = await JSRuntime.InvokeAsync<string>("getPreferredTheme");
            IsDarkMode = preferredTheme == "dark";
        }
    }

    void UpdateLocalStorage()
    {
        LocalStorage.SetItemAsStringAsync("theme", IsDarkMode ? "dark" : "light");
    }
}