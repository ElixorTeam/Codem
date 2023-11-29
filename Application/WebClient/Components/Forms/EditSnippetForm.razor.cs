using System.ComponentModel.DataAnnotations;
using Blazored.Toast.Services;
using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Common;
using WebClient.Components.CodeEditor;
using WebClient.Models;
using WebClient.Services;
using WebClient.Utils;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;
using Сodem.Shared.Enums;

namespace WebClient.Components.Forms;

public sealed partial class EditSnippetForm : ComponentBase
{
    # region Inject
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Inject] private IUserService UserService { get; set; } = null!;
    
    # endregion
    
    # region Parameters
    
    [Parameter, EditorRequired] public CodeSnippetModel Model { get; set; } = null!;
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    [Parameter, EditorRequired] public Guid SnippetId { get; set; }
    
    # endregion
    
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
        string title = string.IsNullOrEmpty(Model.Title) ? "Untitled snippet" : Model.Title;
        string password = Model.Visibility == SnippetVisibilityEnum.ByLink ? Model.Password : string.Empty;
        List<FileDto> codeFiles = CodeFileManager.GetAllFiles().Adapt<List<FileDto>>();
        
        return new()
        {
            Title = title,
            Visibility = Model.Visibility,
            Password = password,
            Files = codeFiles,
            UserId = UserService.GetUser()?.Id,
            ExpireTime = Model.ExpireTime.ToDateTime(TimeOnly.MinValue)
        };
    }

    private async Task UpdateAndHandleSnippet(SnippetDto snippetDto)
    {
        bool allDataEmpty = CodeFileManager.GetAllFiles().All(file => string.IsNullOrEmpty(file.Text));

        if (allDataEmpty)
        {
            ToastService.ShowError("Snippet's files are empty");
            return;
        }
        
        if (Model.ExpireTime <= DateOnly.FromDateTime(DateTime.Today))
        {
            ToastService.ShowError("Snippet expire time can not be less than today date");
            return;
        }

        if (Model.Visibility == SnippetVisibilityEnum.ByLink && string.IsNullOrEmpty(Model.Password))
        {
            ToastService.ShowError("This type of snippet requires password");
            return;
        }
        
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
            ToastService.ShowSuccess("Successfully deleted");
        }
        catch
        {
            ToastService.ShowError("Can not delete snippet");
        }
    }
}