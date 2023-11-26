using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using Сodem.Shared.Dtos.Snippet;

namespace WebAdmin.Pages;

public sealed partial class Snippets : ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    private List<SnippetDto> SnippetsList { get; set; } = new();
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await GetAllUserSnippets();
        StateHasChanged();
    }
    
    private async Task GetAllUserSnippets() =>
        SnippetsList = await SnippetController.GetSnippetListAll();
    
}