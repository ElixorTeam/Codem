using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Common;
using WebClient.Components.CodeEditor;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.Snippet;
using Сodem.Shared.Enums;

namespace WebClient.Pages;

public sealed partial class Viewer : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; }  = null!;
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Inject] private IUserService UserService { get; set; } = null!;
    [Parameter] public Guid Id { get; set; }
    
    private CodeSnippetModel? SnippetModel { get; set; }
    private CodeFileManager CodeFileManager { get; set; } = new();
    private bool IsLoading { get; set; } = true;
    private bool IsBlockedByPassword { get; set; }
    private bool IsOwner { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await InitializeSnippet();
        StateHasChanged();
    }
    
    private async Task InitializeSnippet()
    {
        SnippetDto snippetDto = await GetSnippet();
        SnippetModel = snippetDto.Adapt<CodeSnippetModel>();
        string? UserId = UserService.GetUser()?.Id;
        IsOwner = UserId != null && SnippetModel.UserId == UserId;
        bool IsLimited = SnippetModel.Visibility == SnippetVisibilityEnum.ByLink &&
                         !string.IsNullOrEmpty(SnippetModel.Password);
        IsBlockedByPassword = !IsOwner && IsLimited;
        CodeFileManager = new(SnippetModel.Files);
        IsLoading = false;
    }

    private async Task<SnippetDto> GetSnippet()
    {
        try
        {
            return await SnippetController.GetSnippetById(Id);
        }
        catch
        {
            NavigationManager.NavigateTo(RouteUtils.Home);
            return new();
        }
    }

    private void RedirectToHome() => NavigationManager.NavigateTo(RouteUtils.Home);

    private void UnblockSnippet()
    {
        IsBlockedByPassword = false;
        StateHasChanged();
    }
}