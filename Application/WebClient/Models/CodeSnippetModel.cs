using System.ComponentModel.DataAnnotations;

namespace WebClient.Models;

public sealed class CodeSnippetModel
{
    [MaxLength(64, ErrorMessage = "Title length can not exceed 64 characters")]
    public string Title { get; set; } = string.Empty;

    [Required] 
    public SnippetExpiration ExpireTime { get; set; } = SnippetExpiration.OneWeek;

    public bool IsPrivate { get; set; }

    [RegularExpression(@"^.{4,32}$", ErrorMessage = "Password must be between 4 and 32 characters")]
    public string Password { get; set; } = string.Empty;
    
    public List<CodeFileModel> Files { get; set; } = new();
}