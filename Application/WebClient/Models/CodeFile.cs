namespace WebClient.Models;

public sealed class CodeFile
{ 
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}