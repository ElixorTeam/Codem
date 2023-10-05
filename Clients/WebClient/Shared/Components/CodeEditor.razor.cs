using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebClient.Shared.Components;

public class CodeFile
{
    public Guid Id { get; }
    public string Text { get; set; }
    public string Title { get; set; }

    public CodeFile(string text, string title)
    {
        Id = Guid.NewGuid();
        Text = text;
        Title = title;
    }
}

public partial class CodeEditor : ComponentBase
{

    #region Parameters

    [Parameter] public string ActiveLanguage { get; set; }
    [Parameter] public bool IsReadOnly { get; set; }

    #endregion

    private string InputFileName { get; set; } = String.Empty; 
    private List<CodeFile> Files { get; } = new();
    private Guid CurrentId { get; set; }
    private CodeFile CurrentFile => 
        Files.FirstOrDefault(f => f.Id == CurrentId) ?? GetFirstFileOrDefault();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        CreateFirstFileIfNotExists();
        CurrentId = Files.Last().Id;
    }
    
    // First file trouble shooting
    private void CreateFirstFileIfNotExists()
    {
        if (Files.Count == 0) 
            AddFile();
    }

    private CodeFile GetFirstFileOrDefault()
    {
        CreateFirstFileIfNotExists();
        return Files.First();
    }

    // File functions
    private void SwitchFile(Guid id)
    {
        CurrentId = id;
    }

    private void AddFile(string text = "", string title = "new file")
    {
        CodeFile newFile = new(text, title);
        Files.Add(newFile);
        CurrentId = newFile.Id;
    }

    private void DeleteFile()
    {
        int fileIndex = Files.FindIndex(f => f.Id == CurrentId);
        Files.RemoveAt(fileIndex);
        StateHasChanged();
        if (fileIndex == 0)
        {
            CreateFirstFileIfNotExists();
            return;
        }
        int nextFileIndex = fileIndex >= Files.Count ? fileIndex - 1 : fileIndex;
        CurrentId = Files[nextFileIndex].Id;
    }

    private void ChangeFileName()
    {
        if (string.IsNullOrEmpty(InputFileName)) return;
        CodeFile file = Files.FirstOrDefault(f => f.Id == CurrentId);
        if (file == null) return;
        {
            file.Title = InputFileName;
        }
        InputFileName = String.Empty;
    }
}