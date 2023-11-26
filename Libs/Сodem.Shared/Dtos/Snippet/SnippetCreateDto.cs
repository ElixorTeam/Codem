using Сodem.Shared.Dtos.File;
using Сodem.Shared.Enums;

namespace Сodem.Shared.Dtos.Snippet;

public class SnippetCreateDto
{ 
    public string Title { get; set; }
    public string? UserId { get; set; } = null;
    public SnippetVisibilityEnum Visibility { get; set; }
    public string? Password { get; set; }
    
    public List<FileCreateDto> Files { get; set; }
    
    public SnippetCreateDto()
    {
        Title = string.Empty;
        Visibility = SnippetVisibilityEnum.Public;
        Password = null;
        Files = new();
    }
}