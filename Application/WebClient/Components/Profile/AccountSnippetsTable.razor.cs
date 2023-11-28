using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Common;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Components.Profile;

public sealed partial class AccountSnippetsTable: ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Inject] private IUserService UserService { get; set; } = null!;
    
    private List<CodeSnippetModel> SnippetModels { get; set; } = new();
    private bool IsLoading { get; set; } = true;
    private int CurrentPage { get; set; } = 1;
    private int TotalPages { get; set; } = 1;
    
    private const int MaxItemsPerPage = 10;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await GetAllUserSnippets();
        TotalPages = (int)Math.Ceiling((double)SnippetModels.Count / MaxItemsPerPage);
        IsLoading = false;
        StateHasChanged();
    }

    public void DeleteCallbackAction(int fileIndex)
    {
        SnippetModels.RemoveAt(fileIndex);
        if (CurrentPage > 1 && (CurrentPage - 1) * MaxItemsPerPage >= SnippetModels.Count)
            CurrentPage -= 1;
        TotalPages = (int)Math.Ceiling((double)SnippetModels.Count / MaxItemsPerPage);
        StateHasChanged();
    }

    private async Task GetAllUserSnippets()
    {
        string? userId = UserService.GetUser()?.Id;
        SnippetModels = userId == null ? new() : await GetSnippetsByUser(userId);
        StateHasChanged();
    }

    private async Task<List<CodeSnippetModel>> GetSnippetsByUser(string userId) =>
        (await SnippetController.GetSnippetListByUser(userId)).Adapt<List<CodeSnippetModel>>();
        
    
    private void ChangePage(int newPageOffset)
    {
        int newPage = CurrentPage + newPageOffset;
        if (newPage < 1 || newPage > TotalPages) return;
        CurrentPage = newPage;
        StateHasChanged();
    }
}