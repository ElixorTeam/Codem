using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WebClient.Utils;

namespace WebClient.Shared.Components;

public sealed partial class HeaderSearch : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; }

    private string _searchQuery = String.Empty; 
    
    private void RedirectToSearchByEnter(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
            RedirectToSearch();
    }

    private void RedirectToSearch()
    {
        string url = RouteUtils.Search;
        if (!(string.IsNullOrEmpty(_searchQuery))) 
            url = $"{url}?searchQuery={_searchQuery}";
        _searchQuery = String.Empty;
        NavigationManager.NavigateTo(url);
    }
}