using System.ComponentModel.DataAnnotations;
using Blazored.Toast.Services;
using Codem.Api.Controllers;
using Mapster;
using Mapster.Utils;
using Microsoft.AspNetCore.Components;
using WebClient.Components.CodeEditor;
using Сodem.Shared.Models;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Components;

public sealed partial class CreateSnippetForm : ComponentBase
{
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    
    [Parameter] public EventCallback<string> ActiveLanguageChanged { get; set; }
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    
    private CodeSnippet Model { get; set; } = new();

    private static Array ExpireTimeList { get; } = Enum.GetValues(typeof(SnippetExpiration));

    private async Task HandleSubmit()
    {
        List<ValidationResult> validationResults = PerformModelValidation();
        if (validationResults.Any())
        {
            ShowValidationErrors(validationResults);
            return;
        }

        SnippetCreateDto snippetDto = CreateSnippetDto();
        await CreateAndHandleSnippet(snippetDto);
    }
    
    private List<ValidationResult> PerformModelValidation()
    {
        ValidationContext validationContext = new ValidationContext(Model, null, null);
        List<ValidationResult> validationResults = new();
        Validator.TryValidateObject(Model, validationContext, validationResults, true);
        return validationResults;
    }
    
    private void ShowValidationErrors(List<ValidationResult> validationResults)
    {
        foreach (ValidationResult validationResult in validationResults)
            ToastService.ShowError(validationResult.ErrorMessage ?? string.Empty);
    }
    
    private SnippetCreateDto CreateSnippetDto()
    {
        // DateTime finalDate = DateTime.Now.Add(Model.ExpireTime.ToTimeSpan());
        string title = string.IsNullOrEmpty(Model.Title) ? "Untitled snippet" : Model.Title;
        List<FileCreateDto> codeFiles = CodeFileManager.GetAllFiles().Adapt<List<FileCreateDto>>();
        
        return new SnippetCreateDto
        {
            Title = title,
            IsPrivate = Model.IsPrivate,
            Password = Model.Password,
            Files = codeFiles,
        };
    }
    
    private async Task CreateAndHandleSnippet(SnippetCreateDto snippetDto)
    {
        try
        {
            await SnippetController.CreateSnippet(snippetDto);
            ToastService.ShowSuccess("Successfully added");
        }
        catch
        {
            ToastService.ShowError("An error occurred. Try again.");
        }
    }
}