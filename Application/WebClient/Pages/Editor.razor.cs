using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Common;
using WebClient.Components.CodeEditor;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Pages;

public sealed partial class Editor: ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; }  = null!;
    [Inject] private IUserService UserService { get; set; } = null!;
    
    [Parameter] public Guid Id { get; set; }
    
    private CodeFileManager CodeFileManager { get; set; } = null!;
    private CodeSnippetModel? SnippetModel { get; set; }
    private bool IsLoading { get; set; } = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        SnippetDto snippetDto = await SnippetController.GetSnippetById(Id);
        string? userId = UserService.GetUser()?.Id;
        if (!(userId != null && snippetDto.UserId == userId))
        {
            NavigationManager.NavigateTo(RouteUtils.Home);
            return;
        }
        SnippetModel = snippetDto.Adapt<CodeSnippetModel>();
        CodeFileManager = new(SnippetModel.Files);
        IsLoading = false;
        StateHasChanged();
    }
}