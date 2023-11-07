using Сodem.Shared.Dtos.File;

namespace Сodem.Shared.Dtos.Snippet;

public class SnippetCreateDto : SnippetDto
{ 
    public string Name { get; set; }
    public bool IsPrivate { get; set; }
    public string? Password { get; set; }
    
    public List<FileCreateDto> Files { get; set; }
    
    public SnippetCreateDto()
    {
        Name = string.Empty;
        IsPrivate = false;
        Password = null;
        Files = new();
    }
}