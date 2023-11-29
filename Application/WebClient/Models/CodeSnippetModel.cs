using System.ComponentModel.DataAnnotations;
using Сodem.Shared.Enums;

namespace WebClient.Models;

public sealed class CodeSnippetModel
{
    public Guid Id { get; set; } = Guid.Empty;
    
    [MaxLength(64, ErrorMessage = "Title length can not exceed 64 characters")]
    public string Title { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateOnly CreateDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly ExpireTime { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
    public SnippetVisibilityEnum Visibility { get; set; } = SnippetVisibilityEnum.Public;

    [RegularExpression("^.{4,24}$", ErrorMessage = "Password must be between 4 and 24 characters")]
    public string Password { get; set; } = string.Empty;
    public List<CodeFileModel> Files { get; set; } = new();
}