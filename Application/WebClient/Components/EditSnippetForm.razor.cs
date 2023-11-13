using System.ComponentModel.DataAnnotations;
using Blazored.Toast.Services;
using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Components.CodeEditor;
using Сodem.Shared.Models;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Components;

public sealed partial class EditSnippetForm : ComponentBase
{
    # region Inject
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    
    # endregion
    
    # region Parameters
    
    [Parameter, EditorRequired] public CodeSnippet Model { get; set; } = null!;
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    [Parameter, EditorRequired] public Guid SnippetId { get; set; }
    [Parameter] public EventCallback<string> ActiveLanguageChanged { get; set; }
    
    # endregion
    
    private static Array ExpireTimeList { get; } = Enum.GetValues(typeof(SnippetExpiration));
    
    private async Task HandleSubmit()
    {
        List<ValidationResult> validationResults = PerformModelValidation();
        if (validationResults.Any())
        {
            ShowValidationErrors(validationResults);
            return;
        }

        SnippetDto snippetDto = CreateSnippetDto();
        await UpdateAndHandleSnippet(snippetDto);
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
    
    private SnippetDto CreateSnippetDto()
    {
        // DateTime finalDate = DateTime.Now.Add(Model.ExpireTime.ToTimeSpan());
        string title = string.IsNullOrEmpty(Model.Title) ? "Untitled snippet" : Model.Title;
        List<FileDto> codeFiles = CodeFileManager.GetAllFiles().Adapt<List<FileDto>>();
        
        return new SnippetDto
        {
            Title = title,
            IsPrivate = Model.IsPrivate,
            Password = Model.Password,
            Files = codeFiles,
        };
    }

    private async Task UpdateAndHandleSnippet(SnippetDto snippetDto)
    {
        try
        {
            await SnippetController.UpdateSnippet(snippetDto);
            NavigationManager.NavigateTo($"{RouteUtils.Profile}/{SnippetId}");
            ToastService.ShowSuccess("Successfully edited");
        }
        catch
        {
            ToastService.ShowError("Errors in form");
        }
    }
    
    private async Task HandleDelete()
    {
        try
        {
            await SnippetController.DeleteSnippet(SnippetId);
            NavigationManager.NavigateTo(RouteUtils.Profile);
        }
        catch
        {
            ToastService.ShowError("Can not delete snippet");
        }
    }
}