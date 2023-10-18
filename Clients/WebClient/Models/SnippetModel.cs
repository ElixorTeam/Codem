using System.ComponentModel.DataAnnotations;

namespace WebClient.Models;

public sealed class SnippetModel
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    public TimeSpan ExpireTime { get; set; }

    public bool IsPrivate { get; set; }

    [StringLength(32)]
    public string Password { get; set; }
}