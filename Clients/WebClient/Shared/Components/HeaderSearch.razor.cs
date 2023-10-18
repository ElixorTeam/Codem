using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WebClient.Utils;

namespace WebClient.Shared.Components;

public sealed partial class HeaderSearch : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; }

    private string _searchQuery = ""; 
    
    private void RedirectToSearch(KeyboardEventArgs e)
    {
        if (e.Code is not ("Enter" or "NumpadEnter")) 
            return;
        string url = RouteUtils.Search;
        if (!string.IsNullOrEmpty(_searchQuery)) 
            url = $"{url}?searchQuery={_searchQuery}";
        _searchQuery = string.Empty;
        NavigationManager.NavigateTo(url);
    }
}