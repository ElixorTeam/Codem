using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Components.CodeEditor;
using WebClient.Models;

namespace WebClient.Pages;

public sealed partial class Editor: ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Parameter] public Guid Id { get; set; }
    
    private CodeFileManager CodeFileManager { get; set; } = null!;
    private CodeSnippetModel? SnippetModel { get; set; }
    private bool IsLoading { get; set; } = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        SnippetModel = (await SnippetController.GetSnippetById(Id)).Adapt<CodeSnippetModel>();
        CodeFileManager = new(SnippetModel.Files);
        IsLoading = false;
        StateHasChanged();
    }
}