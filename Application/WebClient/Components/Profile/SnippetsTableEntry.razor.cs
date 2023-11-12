using Blazored.Toast.Services;
using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Components.Profile;

public sealed partial class SnippetsTableEntry : ComponentBase
{
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Parameter, EditorRequired] public Action TableInvokeAction { get; set; } = null!;
    [Parameter, EditorRequired] public SnippetTableModel Snippet { get; set; } = null!;

    private async void DeleteFile()
    {
        try
        {
            await SnippetController.DeleteSnippet(Snippet.Id);
            TableInvokeAction.Invoke();
        }
        catch
        {
            ToastService.ShowError("Error while deleting file");
        }
        
    }
}