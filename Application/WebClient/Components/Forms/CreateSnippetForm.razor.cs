﻿using System.ComponentModel.DataAnnotations;
using Blazored.Toast.Services;
using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
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
        // DateTime finalDate = DateTime.Now.Add(Model.ExpireTime.ToTimeSpan());
        string title = string.IsNullOrEmpty(Model.Title) ? "Untitled snippet" : Model.Title;
        string password = Model.Visibility == SnippetVisibilityEnum.ByLink ? Model.Password : string.Empty;
        List<FileCreateDto> codeFiles = CodeFileManager.GetAllFiles().Adapt<List<FileCreateDto>>();
        
        return new SnippetCreateDto
        {
            Title = title,
            Visibility = Model.Visibility,
            Password = password,
            Files = codeFiles,
        };
    }
    
    private async Task CreateAndHandleSnippet(SnippetCreateDto snippetDto)
    {
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