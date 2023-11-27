using Blazored.Toast.Services;
using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using WebClient.Models;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Components.Profile;

public sealed partial class SnippetsTableEntry : ComponentBase
{
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Parameter, EditorRequired] public Action DeleteCallbackAction { get; set; } = null!;
    [Parameter, EditorRequired] public SnippetDto Snippet { get; set; } = null!;

    private async void DeleteFile()
    {
        try
        {
            await SnippetController.DeleteSnippet(Snippet.Id);
            DeleteCallbackAction.Invoke();
        }
        catch
        {
            ToastService.ShowError("Error while deleting file");
        }
    }
}