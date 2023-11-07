using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Pages;

public sealed partial class Viewer : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; }  = null!;
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Parameter] public Guid Id { get; set; }
    private SnippetDto? SnippetDto { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        GetSnippet();
        StateHasChanged();
    }

    private List<CodeFileModel> ConvertFileList()
    {
        if (SnippetDto == null) return new List<CodeFileModel>();
        return SnippetDto.Files.Select(file => 
            new CodeFileModel(file.Data, file.Name, "Markdown")).ToList();
    }

    private async void GetSnippet()
    {
        try
        {
            SnippetDto = await SnippetController.GetSnippetById(Id);
        }
        catch
        {
            NavigationManager.NavigateTo(@RouteUtils.Home);
        }
        
    }
}