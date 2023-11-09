using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using WebClient.Components.CodeEditor;
using WebClient.Models;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Pages;

public sealed partial class Editor: ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Parameter] public Guid Id { get; set; }
    
    private CodeFileManager CodeFileManager { get; set; } = new();
    private SnippetDto? SnippetDto { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        GetEditSnippet();
    }

    private async void GetEditSnippet()
    {
        SnippetDto = await SnippetController.GetSnippetById(Id);
        StateHasChanged();
    }
    
    private List<CodeFileModel> ConvertFileList(SnippetDto snippetDto)
    {
        return snippetDto.Files.Select(file => 
            new CodeFileModel(file.Data, file.Name, "Markdown")).ToList();
    }

    private SnippetModel ConvertModel(SnippetDto snippetDto)
    {
        return new()
        {
            Title = snippetDto.Title,
            ExpireTime = TimeSpan.FromHours(1),
            IsPrivate = snippetDto.IsPrivate,
            Password = snippetDto.Password ?? string.Empty
        };
    }
}