using System.Collections.Specialized;
using System.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Pages;

public partial class Search : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    public string SearchQuery { get; set; }
    
    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += HandleLocationChanged;
        SearchQuery = getSearchQuery();
    }
    
    private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        SearchQuery = getSearchQuery();
        StateHasChanged();
    }

    private string getSearchQuery()
    {
        Uri uri = new Uri(NavigationManager.Uri);
        NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
        string? searchQuery = query.Get("searchQuery");
        if (string.IsNullOrEmpty(searchQuery)) return "";
        return searchQuery;
    }
    
    private void RedirectToSearch(KeyboardEventArgs e)
    {
        if (!(e.Code == "Enter" || e.Code == "NumpadEnter")) return;
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