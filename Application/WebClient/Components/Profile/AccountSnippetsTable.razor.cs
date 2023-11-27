using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Components.Profile;

public sealed partial class AccountSnippetsTable: ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    private List<SnippetDto> SnippetsDtos { get; set; } = new();
    private bool IsLoading { get; set; } = true;
    private int CurrentPage { get; set; } = 1;
    private int TotalPages { get; set; } = 1;
    
    private const int MaxItemsPerPage = 10;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await GetAllUserSnippets();
        TotalPages = (int)Math.Ceiling((double)SnippetsDtos.Count / MaxItemsPerPage);
        IsLoading = false;
        StateHasChanged();
    }

    public void DeleteCallbackAction(int fileIndex)
    {
        SnippetsDtos.RemoveAt(fileIndex);
        if (CurrentPage > 1 && (CurrentPage - 1) * MaxItemsPerPage >= SnippetsDtos.Count)
            CurrentPage -= 1;
        TotalPages = (int)Math.Ceiling((double)SnippetsDtos.Count / MaxItemsPerPage);
        StateHasChanged();
    }

    private async Task GetAllUserSnippets()
    {
        SnippetsDtos = await SnippetController.GetSnippetPublicList();
        StateHasChanged();
    }
    
    private void ChangePage(int newPageOffset)
    {
        int newPage = CurrentPage + newPageOffset;
        if (newPage < 1 || newPage > TotalPages) return;
        CurrentPage = newPage;
        StateHasChanged();
    }
}