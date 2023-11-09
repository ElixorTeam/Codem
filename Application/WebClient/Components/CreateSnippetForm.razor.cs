using System.ComponentModel.DataAnnotations;
using Blazored.Toast.Services;
using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Components.CodeEditor;
using Сodem.Shared.Models;
using WebClient.Models;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Components;

public sealed partial class CreateSnippetForm : ComponentBase
{
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Parameter] public EventCallback<string> ActiveLanguageChanged { get; set; }
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    private CodeSnippet Model { get; set; }
    private List<ValueTypeModel<TimeSpan>> ExpireTimeList { get; set; }
    
    public CreateSnippetForm()
    {
        ExpireTimeList = new List<ValueTypeModel<TimeSpan>>
        {
            new("Never", TimeSpan.FromDays(365)),
            new("1 hour", TimeSpan.FromHours(1)),
            new("1 day", TimeSpan.FromDays(1)),
            new("1 week", TimeSpan.FromDays(7)),
            new("1 month", TimeSpan.FromDays(31))
        };
        
        Model = new CodeSnippet
        {
            ExpireTime = ExpireTimeList[0].Value,
            Title = string.Empty,
            IsPrivate = false,
            Password = string.Empty
        };
    }

    private async void HandleSubmit()
    {
        DateTime finalDate = DateTime.Now.Add(Model.ExpireTime);
        IList<CodeFile> codeFiles = CodeFileManager.GetAllFiles();
        List<FileCreateDto> filesDto = codeFiles.Adapt<List<FileCreateDto>>();
        SnippetCreateDto snippetDto = new SnippetCreateDto()
        {
            Title = Model.Title,
            IsPrivate = Model.IsPrivate,
            Password = Model.Password,
            Files = filesDto,
        };
        
        try
        {
            await SnippetController.CreateSnippet(snippetDto);
            ToastService.ShowSuccess("Successfully added");
        }
        catch
        {
            ToastService.ShowError("Errors in form");
        }
        
    }
}