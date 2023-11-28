using WebClient.Models;
using Сodem.Shared.Enums;

namespace WebClient.Utils;

public static class LangUsageAnalyzer
{
    public static List<ProgrammingLanguage> GetMostUsedLanguages(List<CodeFileModel> codeFileModels, int numberOfLanguages = 1)
    {
        Dictionary<ProgrammingLanguage, int> languageUsage = new();
        foreach (CodeFileModel file in codeFileModels)
        {
            languageUsage.TryAdd(file.Language, 0);
            languageUsage[file.Language] += file.Text.Length;
        }
        IOrderedEnumerable<KeyValuePair<ProgrammingLanguage, int>> sortedLanguages = languageUsage.OrderByDescending(x => x.Value);
        IEnumerable<KeyValuePair<ProgrammingLanguage, int>> topLanguages = sortedLanguages.Take(numberOfLanguages);
        return topLanguages.Select(x => x.Key).ToList();
    }
}