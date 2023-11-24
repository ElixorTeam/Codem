using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Components.CodeEditor;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Pages;

public sealed partial class Viewer : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; }  = null!;
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Parameter] public Guid Id { get; set; }
    private CodeSnippetModel? SnippetModel { get; set; }
    private CodeFileManager CodeFileManager { get; set; } = new();
    private bool IsLoading { get; set; } = true;
    private bool IsBlockedByPassword { get; set; } = false;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        SnippetModel = (await GetSnippet()).Adapt<CodeSnippetModel>();
        if (SnippetModel.IsPrivate) IsBlockedByPassword = true;
        CodeFileManager = new CodeFileManager(SnippetModel.Files);
        IsLoading = false;
        StateHasChanged();
    }

    private async Task<SnippetDto> GetSnippet()
    {
        try
        {
            return await SnippetController.GetSnippetById(Id);
        }
        catch
        {
            NavigationManager.NavigateTo(@RouteUtils.Home);
            return new SnippetDto();
        }
    }

    private void RedirectToHome() => NavigationManager.NavigateTo(RouteUtils.Home);

    private void UnblockSnippet()
    {
        IsBlockedByPassword = false;
        StateHasChanged();
    }
}