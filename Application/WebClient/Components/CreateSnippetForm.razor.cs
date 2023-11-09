using System.ComponentModel.DataAnnotations;
using Blazored.Toast.Services;
using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using WebClient.Components.CodeEditor;
using Сodem.Shared.Models;
using WebClient.Models;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Components;

public sealed partial class CreateSnippetForm : ComponentBase
{
    [Inject] private IToastService ToastService { get; set; }
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Parameter] public EventCallback<string> ActiveLanguageChanged { get; set; }
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    private SnippetModel Model { get; set; }
    private List<ValueTypeModel<TimeSpan>> ExpireTimeList { get; set; }
    
    public CreateSnippetForm()
    {
        ExpireTimeList = new()
        {
            new("Never", TimeSpan.FromDays(365)),
            new("1 hour", TimeSpan.FromHours(1)),
            new("1 day", TimeSpan.FromDays(1)),
            new("1 week", TimeSpan.FromDays(7)),
            new("1 month", TimeSpan.FromDays(31))
        };
        
        Model = new()
        {
            ExpireTime = ExpireTimeList[0].Value,
            Title = string.Empty,
            IsPrivate = false,
            Password = string.Empty
        };
    }

    private List<FileCreateDto> ConvertToFileDto(IList<CodeFileModel> fileModelList)
    {
        return fileModelList.Select(file => 
            new FileCreateDto { Data = file.Text, Name = file.Title }).ToList();
    }

    private async void HandleSubmit()
    {
        DateTime finalDate = DateTime.Now.Add(Model.ExpireTime);
        List<FileCreateDto> files = ConvertToFileDto(CodeFileManager.GetAllFiles());
        try
        {
            await SnippetController.CreateSnippet(new()
            {
                Files = files,
                Title = Model.Title,
                IsPrivate = Model.IsPrivate,
                Password = Model.Password,
            });
        }
        catch
        {
            Console.WriteLine("error");
        }
        ToastService.ShowError("Adding form");
    }
}