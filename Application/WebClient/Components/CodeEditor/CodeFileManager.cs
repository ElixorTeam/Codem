using WebClient.Models;

namespace WebClient.Components.CodeEditor;

public class CodeFileManager
{
    private const int MaxFilesCount = 10;
    private List<CodeFileModel> Files { get; } = new();
    public Action? OnFileChange { get; set; }
    private Guid CurrentId { get; set; }

    public CodeFileManager()
    {
        CreateFirstFileIfNotExists();
        CurrentId = Files.First().Id;
    }

    private void CreateFirstFileIfNotExists()
    {
        if (Files.Count == 0)
            AddFile();
    }

    public CodeFileModel GetCurrentFile() => 
        Files.FirstOrDefault(f => f.Id == CurrentId) ?? GetFirstFileOrDefault();

    public List<CodeFileModel> GetAllFiles() => Files;
    
    public void SwitchFile(Guid id)
    {
        CurrentId = id;
        OnFileChange?.Invoke();
    }

    public void AddFile(string text = "", string title = "new file", string lang = "Markdown")
    {
        if (Files.Count >= MaxFilesCount) return;
        CodeFileModel newFile = new(text, title, lang);
        Files.Add(newFile);
        CurrentId = newFile.Id;
        OnFileChange?.Invoke();
    }

    public void DeleteFile(Guid id)
    {
        int fileIndex = Files.FindIndex(f => f.Id == id);
        Files.RemoveAt(fileIndex);
        if (fileIndex == 0)
        {
            CreateFirstFileIfNotExists();
            OnFileChange?.Invoke();
            return;
        }

        int nextFileIndex = fileIndex >= Files.Count ? fileIndex - 1 : fileIndex;
        CurrentId = Files[nextFileIndex].Id;
        OnFileChange?.Invoke();
    }

    public void ChangeFileName(Guid id, string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return;
        int fileIndex = Files.FindIndex(f => f.Id == id);
        Files[fileIndex].Title = fileName;
        OnFileChange?.Invoke();
    }

    public void ChangeCurrentLanguage(string lang)
    {
        if (string.IsNullOrEmpty(lang)) return;
        CodeFileModel file = GetCurrentFile();
        file.Language = lang;
        OnFileChange?.Invoke();
    }

    private CodeFileModel GetFirstFileOrDefault()
    {
        CreateFirstFileIfNotExists();
        return Files.First();
    }
}