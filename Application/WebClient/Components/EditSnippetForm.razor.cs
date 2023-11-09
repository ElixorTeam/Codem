using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using WebClient.Components.CodeEditor;
using Сodem.Shared.Models;
using WebClient.Models;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Components;

public sealed partial class EditSnippetForm : ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Parameter, EditorRequired] public SnippetModel Model { get; set; } = null!;
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    [Parameter, EditorRequired] public Guid SnippetId { get; set; }
    
    [Parameter] public EventCallback<string> ActiveLanguageChanged { get; set; }
    private List<ValueTypeModel<TimeSpan>> ExpireTimeList { get; } = new()
    {
        new ValueTypeModel<TimeSpan>("Never", TimeSpan.FromDays(365)),
        new ValueTypeModel<TimeSpan>("1 hour", TimeSpan.FromHours(1)),
        new ValueTypeModel<TimeSpan>("1 day", TimeSpan.FromDays(1)),
        new ValueTypeModel<TimeSpan>("1 week", TimeSpan.FromDays(7)),
        new ValueTypeModel<TimeSpan>("1 month", TimeSpan.FromDays(31))
    };
    
    private List<FileDto> ConvertToFileDto(IList<CodeFileModel> fileModelList)
    {
        return fileModelList.Select(file => 
            new FileDto { Data = file.Text, Name = file.Title }).ToList();
    }

    private async void HandleSubmit()
    {
        DateTime finalDate = DateTime.Now.Add(Model.ExpireTime);
        List<FileDto> files = ConvertToFileDto(CodeFileManager.GetAllFiles());
        SnippetDto snippetDto = new SnippetDto
        {
            Id = SnippetId,
            Name = Model.Title,
            IsPrivate = Model.IsPrivate,
            Password = Model.Password,
            Files = files,
        };
        
        try
        {
            await SnippetController.UpdateSnippet(snippetDto);
        }
        catch
        {
            Console.WriteLine("error");
        }
    }
}