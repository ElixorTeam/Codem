using System.ComponentModel.DataAnnotations;
using Blazored.Toast.Services;
using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Common;
using WebClient.Components.CodeEditor;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;
using Сodem.Shared.Enums;

namespace WebClient.Components.Forms;

public sealed partial class CreateSnippetForm : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Inject] private IUserService UserService { get; set; } = null!;
    
    [Parameter, EditorRequired] public CodeFileManager CodeFileManager { get; set; } = null!;
    private CodeSnippetModel Model { get; set; } = new();

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
        ValidationContext validationContext = new(Model, null, null);
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
        string title = string.IsNullOrEmpty(Model.Title) ? "Untitled snippet" : Model.Title;
        string password = Model.Visibility == SnippetVisibilityEnum.ByLink ? Model.Password : string.Empty;
        List<FileCreateDto> codeFiles = CodeFileManager.GetAllFiles().Adapt<List<FileCreateDto>>();
        
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
    
    private async Task CreateAndHandleSnippet(SnippetCreateDto snippetDto)
    {
        bool allDataEmpty = CodeFileManager.GetAllFiles().All(file => string.IsNullOrEmpty(file.Text));

        if (allDataEmpty)
        {
            ToastService.ShowError("Snippet's files are empty");
            return;
        }
        
        try
        {
            SnippetDto snippet = await SnippetController.CreateSnippet(snippetDto);
            NavigationManager.NavigateTo($"{RouteUtils.Snippet}/{snippet.Id}");
            ToastService.ShowSuccess("Successfully added");
        }
        catch
        {
            ToastService.ShowError("An error occurred. Try again.");
        }
    }
}