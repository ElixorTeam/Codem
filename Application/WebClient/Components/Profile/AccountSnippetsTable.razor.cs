using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Components.Profile;

public sealed partial class AccountSnippetsTable: ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    private List<SnippetDto> SnippetsList { get; set; } = new();
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        GetAllUserSnippets();
    }

    public void TableInvokeAction(int fileIndex)
    {
        SnippetsList.RemoveAt(fileIndex);
        StateHasChanged();
    }

    private async void GetAllUserSnippets()
    {
        SnippetsList = await SnippetController.GetSnippetListAll();
        StateHasChanged();
    }

    private SnippetTableModel ConvertModel(SnippetDto snippet)
    {
        return new()
        {
            Id=snippet.Id,
            Title=snippet.Title,
            PublicDate=DateOnly.FromDateTime(DateTime.Today),
            ExpireTime="1 week",
            Views=1400,
            Stars=100,
            ProgramLanguage="Markdown"
        };
    }
}