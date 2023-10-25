namespace WebClient.Models;

public sealed class CodeFileModel
{ 
    public Guid Id { get; init; }
    public string Text { get; set; }
    public string Title { get; set; }
    

    public CodeFileModel(string text, string title)
    {
        Id = Guid.NewGuid();
        Text = text;
        Title = title;
    }
}