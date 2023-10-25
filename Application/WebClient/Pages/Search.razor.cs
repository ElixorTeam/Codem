using System.Collections.Specialized;
using System.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Pages;

public sealed partial class Search : ComponentBase, IDisposable
{
    [Inject] private NavigationManager NavigationManager { get; set; }

    private string SearchQuery { get; set; }
    
    public Search()
    {
        SearchQuery = string.Empty;
    }
    
    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += HandleLocationChanged;
        SearchQuery = GetSearchQuery();
    }
    
    private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        SearchQuery = GetSearchQuery();
        StateHasChanged();
    }

    private string GetSearchQuery()
    {
        Uri uri = new(NavigationManager.Uri);
        NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
        string? searchQuery = query.Get("searchQuery");
        return string.IsNullOrEmpty(searchQuery) ? "" : searchQuery;
    }
    
    private void RedirectToSearch(KeyboardEventArgs e)
    {
        if (e.Code is not ("Enter" or "NumpadEnter")) return;
        string url = "/search";
        if (!(string.IsNullOrEmpty(SearchQuery))) 
            url = $"{url}?searchQuery={SearchQuery}";
        NavigationManager.NavigateTo(url);
    }
    
    public void Dispose()
    {
        NavigationManager.LocationChanged -= HandleLocationChanged;
    }
}