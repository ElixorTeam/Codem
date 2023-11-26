using System.Collections.Specialized;
using System.Web;
using Blazored.Toast.Services;
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
    private int CurrentPage { get; set; } = 1;
    private int TotalPages { get; set; } = 1;
    private const int MaxItemsPerPage = 10;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await UpdateFilteredList();
    }

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += HandleLocationChanged;
    }

    private async Task UpdateFilteredList()
    {
        SearchQuery = GetSearchQuery();
        SnippetList = await SnippetController.GetSnippetListByName(SearchQuery);
        TotalPages = (int)Math.Ceiling((double)SnippetList.Count / MaxItemsPerPage);
        StateHasChanged();
    }

    private static string GetFirstFileCode(SnippetDto snippet) => 
        snippet.Files.Any() ? snippet.Files.First().Data : string.Empty;
    
    private async void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        SearchQuery = GetSearchQuery();
        await UpdateFilteredList();
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

    private void ChangePage(int newPageOffset)
    {
        int newPage = CurrentPage + newPageOffset;
        if (newPage < 1 || newPage > TotalPages) return;
        CurrentPage = newPage;
        StateHasChanged();
    }

    public void Dispose() => NavigationManager.LocationChanged -= HandleLocationChanged;
}