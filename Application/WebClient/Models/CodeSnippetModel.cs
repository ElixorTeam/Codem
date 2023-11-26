using System.ComponentModel.DataAnnotations;
using Сodem.Shared.Enums;

namespace WebClient.Models;

public sealed class CodeSnippetModel
{
    [MaxLength(64, ErrorMessage = "Title length can not exceed 64 characters")]
    public string Title { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    [Required] 
    public SnippetExpiration ExpireTime { get; set; } = SnippetExpiration.OneWeek;
    public SnippetVisibilityEnum Visibility { get; set; } = SnippetVisibilityEnum.Public;

    [RegularExpression("^.{4,24}$", ErrorMessage = "Password must be between 4 and 24 characters")]
    public string Password { get; set; } = string.Empty;
    public List<CodeFileModel> Files { get; set; } = new();
}