using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using WebClient.Models;

namespace WebClient.Shared.Components;

public partial class CodeEditor : ComponentBase
{

    #region Parameters

    [Parameter] public string ActiveLanguage { get; set; } = "Markdown";
    [Parameter] public bool IsReadOnly { get; set; }
    [Parameter] public bool IsOwner { get; set; }

    #endregion

    private Guid CurrentId { get; set; }
    private string InputFileName { get; set; } = string.Empty; 
    private List<CodeFileModel> Files { get; } = new();
    private CodeFileModel CurrentFile => 
        Files.FirstOrDefault(f => f.Id == CurrentId) ?? GetFirstFileOrDefault();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        CreateFirstFileIfNotExists();
        CurrentId = Files.First().Id;
    }
    
    private void CreateFirstFileIfNotExists()
    {
        if (Files.Count == 0) 
            AddFile();
    }

    private CodeFileModel GetFirstFileOrDefault()
    {
        CreateFirstFileIfNotExists();
        return Files.First();
    }
    
    private void SwitchFile(Guid id)
    {
        CurrentId = id;
    }

    private void AddFile(string text = "", string title = "new file")
    {
        CodeFileModel newFile = new(text, title);
        Files.Add(newFile);
        CurrentId = newFile.Id;
    }

    private void DeleteFile(Guid id)
    {
        int fileIndex = Files.FindIndex(f => f.Id == id);
        Files.RemoveAt(fileIndex);
        if (fileIndex == 0)
        {
            CreateFirstFileIfNotExists();
            return;
        }
        StateHasChanged();
        int nextFileIndex = fileIndex >= Files.Count ? fileIndex - 1 : fileIndex;
        CurrentId = Files[nextFileIndex].Id;
    }

    private void ChangeFileName()
    {
        if (string.IsNullOrEmpty(InputFileName)) return;
        CodeFileModel? file = Files.FirstOrDefault(f => f.Id == CurrentId);
        if (file != null)
            file.Title = InputFileName;
        InputFileName = string.Empty;
    }
}