using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Components.CodeEditor;
using WebClient.Models;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Pages;

public sealed partial class Editor: ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Parameter] public Guid Id { get; set; }
    
    private CodeFileManager CodeFileManager { get; } = new();
    private SnippetDto? SnippetDto { get; set; }
    private bool IsLoading { get; set; } = true;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        GetEditSnippet();
        IsLoading = false;
        StateHasChanged();
    }

    private async void GetEditSnippet()
    {
        SnippetDto = await SnippetController.GetSnippetById(Id);
        StateHasChanged();
    }
    
    private static List<CodeFile> ConvertFileList(SnippetDto snippetDto)
    {
        List<FileDto> fileDtos = snippetDto.Files;
        return fileDtos.Adapt<List<CodeFile>>();
    }

    private static CodeSnippet ConvertModel(SnippetDto snippetDto) => snippetDto.Adapt<CodeSnippet>();
}