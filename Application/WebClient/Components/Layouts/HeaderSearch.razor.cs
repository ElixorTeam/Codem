using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WebClient.Utils;

namespace WebClient.Components.Layouts;

public sealed partial class HeaderSearch : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    private string _searchQuery = string.Empty; 
    
    private void RedirectToSearchByEnter(KeyboardEventArgs e)
    {
        if (e.Code is "Enter" or "NumpadEnter")
            RedirectToSearch();
    }

    private void RedirectToSearch()
    {
        string url = RouteUtils.Search;
        if (!(string.IsNullOrEmpty(_searchQuery))) 
            url = $"{url}?searchQuery={_searchQuery}";
        _searchQuery = string.Empty;
        NavigationManager.NavigateTo(url);
    }
}