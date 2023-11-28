using Blazor.Heroicons;
using Codem.Api.Controllers;
using Mapster;
using Microsoft.AspNetCore.Components;
using WebClient.Common;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.Snippet;
using Сodem.Shared.Enums;
using Сodem.Shared.Models;

namespace WebClient.Components.Profile;

public sealed partial class AccountInfo: ComponentBase
{
    [Inject] private SnippetController SnippetController { get; set; } = null!;
    [Inject] private IUserService UserService { get; set; } = null!;
    
    private UserModel? User { get; set; }
    private List<StatModel> StatList { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        User = UserService.GetUser();
        string topLanguages = await GetTopLanguagesFromAllSnippets();
        StatList = new()
        {
            new() { IconName = HeroiconName.CodeBracket, Value = topLanguages }
            // new() { IconName = @HeroiconName.Eye, Value = "120" },
            // new() { IconName = @HeroiconName.Star, Value = "120" },
        };
    }

    private async Task<string> GetTopLanguagesFromAllSnippets(int numberOfLanguages = 3)
    {
        List<SnippetDto> snippetsDto = await SnippetController.GetSnippetListByUser(UserService.GetUser()?.Id ?? string.Empty);
        List<CodeSnippetModel> snippets = snippetsDto.Adapt<List<CodeSnippetModel>>();
        List<CodeFileModel> allFiles = snippets.SelectMany(snippet => snippet.Files).ToList();
        List<ProgrammingLanguage> mostUsedLanguages = LangUsageAnalyzer.GetMostUsedLanguages(allFiles, numberOfLanguages);
        return string.Join(", ", mostUsedLanguages.Select(language => EnumHelper.GetEnumDescription(language)));
    }
}

internal class StatModel
{
    public string IconName { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
}