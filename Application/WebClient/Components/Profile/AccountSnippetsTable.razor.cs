using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.Snippet;
using Сodem.Shared.Enums;

namespace WebClient.Components.Profile;

public sealed partial class AccountSnippetsTable: ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    private List<SnippetDto> SnippetsList { get; set; } = new();
    private bool IsLoading { get; set; } = true;
    private int CurrentPage { get; set; } = 1;
    private int TotalPages { get; set; } = 1;
    private const int MaxItemsPerPage = 10;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await GetAllUserSnippets();
        TotalPages = (int)Math.Ceiling((double)SnippetsList.Count / MaxItemsPerPage);
        IsLoading = false;
        StateHasChanged();
    }

    public void TableInvokeAction(int fileIndex)
    {
        SnippetsList.RemoveAt(fileIndex);
        if (CurrentPage > 1 && (CurrentPage - 1) * MaxItemsPerPage >= SnippetsList.Count)
            CurrentPage -= 1;
        TotalPages = (int)Math.Ceiling((double)SnippetsList.Count / MaxItemsPerPage);
        StateHasChanged();
    }

    private async Task GetAllUserSnippets()
    {
        SnippetsList = await SnippetController.GetSnippetPublicList();
        StateHasChanged();
    }
    
    private void ChangePage(int newPageOffset)
    {
        int newPage = CurrentPage + newPageOffset;
        if (newPage < 1 || newPage > TotalPages) return;
        CurrentPage = newPage;
        StateHasChanged();
    }

    private static SnippetTableModel ConvertModel(SnippetDto snippet)
    {
        return new()
        {
            Id=snippet.Id,
            Title=snippet.Title,
            PublicDate=DateOnly.FromDateTime(DateTime.Today),
            ExpireTime="1 week",
            Views=1400,
            Stars=100,
            ProgramLanguage=@EnumHelper.GetEnumDescription(snippet.Files.First().ProgrammingLanguage)
        };
    }
}