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
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await GetAllUserSnippets();
        IsLoading = false;
        StateHasChanged();
    }

    public void TableInvokeAction(int fileIndex)
    {
        SnippetsList.RemoveAt(fileIndex);
        StateHasChanged();
    }

    private async Task GetAllUserSnippets()
    {
        SnippetsList = await SnippetController.GetSnippetListAll();
        StateHasChanged();
    }

    private static SnippetTableModel ConvertModel(SnippetDto snippet)
    {
        return new SnippetTableModel
        {
            Id=snippet.Id,
            Title=snippet.Title,
            PublicDate=DateOnly.FromDateTime(DateTime.Today),
            ExpireTime="1 week",
            Views=1400,
            Stars=100,
            ProgramLanguage=@EnumHelper.GetEnumDescription(ProgrammingLanguage.Markdown)
        };
    }
}