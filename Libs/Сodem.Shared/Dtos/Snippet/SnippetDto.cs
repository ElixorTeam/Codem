using Сodem.Shared.Dtos.File;

namespace Сodem.Shared.Dtos.Snippet;

public class SnippetDto
{ 
    public Guid Id { get; set; } = Guid.Empty;
    public string Title { get; set; } = string.Empty;
    public bool IsPrivate { get; set; }
    public string? Password { get; set; }
    public List<FileDto> Files { get; set; } = new();
}