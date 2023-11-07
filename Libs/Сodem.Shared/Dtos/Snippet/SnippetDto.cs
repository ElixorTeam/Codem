using Сodem.Shared.Dtos.File;

namespace Сodem.Shared.Dtos.Snippet;

public class SnippetDto
{ 
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsPrivate { get; set; } = false;
    public string? Password { get; set; } = null;
    public List<FileDto> Files { get; set; } = new();
}