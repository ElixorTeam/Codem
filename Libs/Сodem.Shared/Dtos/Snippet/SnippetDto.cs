using Сodem.Shared.Dtos.File;
using Сodem.Shared.Enums;

namespace Сodem.Shared.Dtos.Snippet;

public class SnippetDto
{ 
    public Guid Id { get; set; } = Guid.Empty;
    public string? UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreateDate = DateTime.Today;
    public SnippetVisibilityEnum Visibility { get; set; } = SnippetVisibilityEnum.Public;
    public string? Password { get; set; }
    public List<FileDto> Files { get; set; } = new();
}