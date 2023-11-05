namespace WebClient.Models;

public sealed class CodeFileModel
{ 
    public Guid Id { get; init; }
    public string Text { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
    

    public CodeFileModel(string text, string title, string language)
    {
        Id = Guid.NewGuid();
        Text = text;
        Title = title;
        Language = language;
    }
}