namespace WebClient.Models;

public sealed class CodeFile
{ 
    public Guid Id { get; }
    public string Text { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
    
    public CodeFile()
    {
        Id = Guid.NewGuid();
    }

    public CodeFile(string text, string title, string language)
        : this()
    {
        Text = text;
        Title = title;
        Language = language;
    }
}