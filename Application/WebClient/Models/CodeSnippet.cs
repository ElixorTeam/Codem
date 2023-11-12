using System.ComponentModel.DataAnnotations;

namespace WebClient.Models;

public sealed class CodeSnippet
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    public TimeSpan ExpireTime { get; set; }

    public bool IsPrivate { get; set; }

    [StringLength(32)]
    public string Password { get; set; }
    
    public List<CodeFile> Files { get; set; } = new();
}