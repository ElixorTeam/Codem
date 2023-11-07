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

    private async void GetAllUserSnippets()
    {
        SnippetsList = await SnippetController.GetSnippetListAll();
        StateHasChanged();
    }

    private SnippetTableModel ConvertModel(SnippetDto snippet)
    {
        return new SnippetTableModel
        {
            Id=snippet.Id,
            Title=snippet.Name,
            PublicDate=DateOnly.FromDateTime(DateTime.Today),
            ExpireTime="1 week",
            Views=1400,
            Stars=100,
            ProgramLanguage="Markdown"
        };
    }
}