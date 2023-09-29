using System.Collections.Specialized;
using System.Web;
using Microsoft.AspNetCore.Components;

namespace WebClient.Pages;

public partial class Search : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    public string SearchQuery { get; set; }
    
    protected override void OnInitialized()
    {
        SearchQuery = getSearchQuery();
    }

    private string getSearchQuery()
    {
        Uri uri = new Uri(NavigationManager.Uri);
        NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
        string? searchQuery = query.Get("searchQuery");
        if (string.IsNullOrEmpty(searchQuery)) return "";
        return searchQuery;
    }
}