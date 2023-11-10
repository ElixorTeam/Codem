using WebClient.Models;

namespace WebClient.Components.CodeEditor;

public class CodeFileManager
{
    private const int MaxFilesCount = 10;
    private IList<CodeFile> Files { get; }
    public Action? OnFileChange { get; set; }
    private Guid CurrentId { get; set; }

    public CodeFileManager(IList<CodeFile>? files = null)
    {
        Files = files ?? new List<CodeFile>();
        CreateFirstFileIfNotExists();
        CurrentId = Files.First().Id;
    }

    public CodeFile GetCurrentFile() => 
        Files.FirstOrDefault(f => f.Id == CurrentId) ?? GetFirstFileOrDefault();

    public IList<CodeFile> GetAllFiles() => Files;
    
    public void SwitchFile(Guid id)
    {
        if (Files.All(f => f.Id != id)) return;
        CurrentId = id;
        OnFileChange?.Invoke();
    }

    public void AddFile(
        string text = "",
        string title = "new_file.txt",
        ProgrammingLanguage lang = ProgrammingLanguage.Markdown)
    {
        if (Files.Count >= MaxFilesCount) 
            throw new InvalidOperationException("Cannot add more files, maximum count reached.");
        
        if (string.IsNullOrEmpty(title)) return;
        CodeFile newFile = new CodeFile { Text = text, Title = title, Language = lang };
        Files.Add(newFile);
        CurrentId = newFile.Id;
        OnFileChange?.Invoke();
    }

    public void DeleteFile(Guid id)
    {
        CodeFile? file = Files.FirstOrDefault(f => f.Id == id);
        if (file == null) return;
        
        int fileIndex = Files.IndexOf(file);
        Files.Remove(file);
        
        if (!Files.Any()) CreateFirstFileIfNotExists();
        else if (file.Id == CurrentId) CurrentId = Files[GetNextFileIndex(fileIndex)].Id;
        
        OnFileChange?.Invoke();
    }

    public void ChangeFileName(Guid id, string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return;
        CodeFile? file = Files.FirstOrDefault(f => f.Id == id);
        if (file == null) return;
        file.Title = fileName;
        OnFileChange?.Invoke();
    }

    public void ChangeLanguageOfCurrentFile(ProgrammingLanguage lang)
    {
        CodeFile file = GetCurrentFile();
        file.Language = lang;
        OnFileChange?.Invoke();
    }

    private int GetNextFileIndex(int currentFileIndex) =>
        currentFileIndex >= Files.Count - 1 ? Files.Count - 1 : currentFileIndex;

    private CodeFile GetFirstFileOrDefault()
    {
        CreateFirstFileIfNotExists();
        return Files.First();
    }
    
    private void CreateFirstFileIfNotExists()
    {
        if (!Files.Any()) AddFile();
    }
}