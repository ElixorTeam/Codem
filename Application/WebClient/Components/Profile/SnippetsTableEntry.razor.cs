using Blazored.Toast.Services;
using Codem.Api.Controllers;
using Microsoft.AspNetCore.Components;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.Snippet;
using Сodem.Shared.Enums;

namespace WebClient.Components.Profile;

public sealed partial class SnippetsTableEntry : ComponentBase
{
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Parameter, EditorRequired] public Action DeleteCallbackAction { get; set; } = null!;
    [Parameter, EditorRequired] public CodeSnippetModel SnippetModel { get; set; } = null!;

    private string MostUsedLanguage()
    {
        List<ProgrammingLanguage> mostUsedLanguages = LangUsageAnalyzer.GetMostUsedLanguages(SnippetModel.Files);
        return EnumHelper.GetEnumDescription(mostUsedLanguages.First());
    }

    private async void DeleteFile()
    {
        try
        {
            await SnippetController.DeleteSnippet(SnippetModel.Id);
            DeleteCallbackAction.Invoke();
        }
        catch
        {
            ToastService.ShowError("Error while deleting file");
        }
    }
}