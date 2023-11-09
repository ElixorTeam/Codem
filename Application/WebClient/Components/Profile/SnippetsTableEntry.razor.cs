using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Components.Profile;

public sealed partial class SnippetsTableEntry : ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; }
    [Parameter, EditorRequired] public SnippetTableModel Snippet { get; set; } = null!;

    private async void DeleteFile()
    {
        await SnippetController.DeleteSnippet(Snippet.Id);
        StateHasChanged();
    }
}