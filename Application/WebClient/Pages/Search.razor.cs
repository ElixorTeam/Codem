using System.Collections.Specialized;
using System.Web;
using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using WebClient.Models;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Pages;

public sealed partial class Search : ComponentBase, IDisposable
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; }  = null!;

    private List<CodeSnippetModel> SnippetModels { get; set; } = new();

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
        List<SnippetDto> snippetDtos = await SnippetController.GetSnippetPublicListByName(SearchQuery);
        SnippetModels = snippetDtos.Adapt<List<CodeSnippetModel>>();
        TotalPages = (int)Math.Ceiling((double)SnippetModels.Count / MaxItemsPerPage);
        StateHasChanged();
    }
    
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