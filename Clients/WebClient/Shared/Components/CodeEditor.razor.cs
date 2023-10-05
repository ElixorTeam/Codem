using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Shared.Components;

public class CodeFile
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Title { get; set; }
}

public partial class CodeEditor : ComponentBase
{
    [Parameter] public string ActiveLanguage { get; set; }
    [Parameter] public bool IsReadOnly { get; set; }

    public List<CodeFile> Files { get; set; }
    public int CurrentId { get; set; }

    public CodeEditor()
    {
        IsReadOnly = false;
        ActiveLanguage = "Markdown";
        CurrentId = 0;
        Files = new List<CodeFile>() {
            new CodeFile { Id = 0, Text = "Hello", Title = "file.txt" }
        };
    }

    public void SwitchFile(int Id)
    {
        CurrentId = Id;
    }

    public void AddFile()
    {
        int NewId = Files.Count;
        CodeFile NewFile = new CodeFile { Id = NewId, Text = "", Title = $"file{NewId}.txt" };
        Files.Add(NewFile);
        CurrentId = NewFile.Id;
    }
}