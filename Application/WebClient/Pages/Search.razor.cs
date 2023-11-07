using System.Collections.Specialized;
using System.Web;
using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Pages;

public sealed partial class Search : ComponentBase, IDisposable
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; }  = null!;

    private List<SnippetDto> SnippetList { get; set; } = new();

    private string SearchQuery { get; set; } = string.Empty;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        UpdateFilteredList();
    }

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += HandleLocationChanged;
    }

    private async void UpdateFilteredList()
    {
        SearchQuery = GetSearchQuery();
        SnippetList = await SnippetController.GetSnippetListByName(SearchQuery);
        StateHasChanged();
    }

    private string getFirstFileCode(SnippetDto snippet) => 
        snippet.Files.Any() ? snippet.Files.First().Data : string.Empty;
    
    private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        SearchQuery = GetSearchQuery();
        UpdateFilteredList();
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

    public void Dispose() => NavigationManager.LocationChanged -= HandleLocationChanged;
}