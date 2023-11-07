using Сodem.Shared.Dtos.File;

namespace Сodem.Shared.Dtos.Snippet;

public class SnippetDto
{ 
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsPrivate { get; set; }
    public string? Password { get; set; }
    public List<FileDto> Files { get; set; }
    
    public SnippetDto()
    {
        Id = Guid.Empty;
        Name = string.Empty;
        IsPrivate = false;
        Password = null;
        Files = new();
    }
}