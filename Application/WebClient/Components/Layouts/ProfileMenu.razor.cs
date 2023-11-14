using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebClient.Utils;
using Blazor.Heroicons;

namespace WebClient.Components.Layouts;

public sealed partial class ProfileMenu: ComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    
    [Parameter] public string ImageUrl { get; set; } = string.Empty;
    [Parameter] public string UserName { get; set; } = string.Empty;
    
    # region Variables
    
    private IJSObjectReference Module { get; set; } = null!;
    private string DropdownUniqueId { get; init; } = Guid.NewGuid().ToString();
    private List<ProfileMenuEntry> MenuEntries { get; init; } = new()
    {
        new ProfileMenuEntry
        {
            Url = RouteUtils.Profile,
            IconName = @HeroiconName.Identification,
            Title = "Profile"
        },
        new ProfileMenuEntry
        {
            Url = RouteUtils.Logout,
            IconName = @HeroiconName.ArrowRightOnRectangle,
            Title = "Logout"
        }
    };
    
    # endregion
    
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await InitializeDropdown();
    }

    private async Task InitializeDropdown()
    {
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/dropdownInterface.js");
        await Module.InvokeVoidAsync("initDropdown", "profileDropdown",
            "profileDropdownButton", DropdownUniqueId);
    }
    
    private async Task HideDropdown() => 
        await Module.InvokeVoidAsync("hideDropdown", DropdownUniqueId);
}

internal class ProfileMenuEntry
{
    public string Url { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string IconName { get; init; } = string.Empty;
}