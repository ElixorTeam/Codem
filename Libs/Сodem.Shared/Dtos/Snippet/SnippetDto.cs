using Сodem.Shared.Dtos.File;
using Сodem.Shared.Enums;

namespace Сodem.Shared.Dtos.Snippet;

public class SnippetDto
{ 
    public Guid Id { get; set; } = Guid.Empty;
    public string? UserId { get; set; } = null;
    public string Title { get; set; } = string.Empty;
    public SnippetVisibilityEnum Visibility { get; set; } = SnippetVisibilityEnum.Public;
    public string? Password { get; set; } = null;
    public List<FileDto> Files { get; set; } = new();
}