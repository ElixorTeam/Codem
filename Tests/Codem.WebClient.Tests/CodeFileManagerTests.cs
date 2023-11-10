using WebClient.Components.CodeEditor;
using WebClient.Models;

namespace Codem.WebClient.Tests;

public class CodeFileManagerTests
{
    [Fact]
    public void GetCurrentFile_ReturnsFirstFile_WhenNoFilesExist()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        
        CodeFile result = codeFileManager.GetCurrentFile();
        
        Assert.NotNull(result);
        Assert.Equal("new_file", result.Title);
    }
    
    [Fact]
    public void AddFile_IncreasesFileCount()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        int initialCount = codeFileManager.GetAllFiles().Count;
        
        codeFileManager.AddFile();
        
        int updatedCount = codeFileManager.GetAllFiles().Count;
        Assert.Equal(initialCount + 1, updatedCount);
    }
    
    [Fact]
    public void AddFile_AddsFileWithDefaultValues()
    {
        CodeFileManager codeFileManager = new CodeFileManager();

        codeFileManager.AddFile();

        CodeFile addedFile = codeFileManager.GetAllFiles().Last();
        Assert.NotNull(addedFile);
        Assert.Equal("new_file", addedFile.Title);
        Assert.Equal("Markdown", addedFile.Language);
    }
    
    [Fact]
    public void AddFile_FailsWhenMaxFilesCountReached()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        const int maxFilesCount = 10;
        for (int i = 0; i < maxFilesCount - 1; i++)
            codeFileManager.AddFile();
        int initialCount = codeFileManager.GetAllFiles().Count;

        Assert.Throws<InvalidOperationException>(() => codeFileManager.AddFile());
        int updatedCount = codeFileManager.GetAllFiles().Count;
        Assert.Equal(initialCount, updatedCount);
    }
    
    [Fact]
    public void DeleteFile_DeleteOnlyFileAndReturnDefault()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        int initialCount = codeFileManager.GetAllFiles().Count;
        CodeFile fileToDelete = codeFileManager.GetAllFiles().First();

        codeFileManager.DeleteFile(fileToDelete.Id);

        int updatedCount = codeFileManager.GetAllFiles().Count;
        Assert.Equal(initialCount, updatedCount);
        Assert.DoesNotContain(fileToDelete, codeFileManager.GetAllFiles());
    }
    
    [Fact]
    public void DeleteFile_NonExistentFile()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        IList<CodeFile> initialFiles = codeFileManager.GetAllFiles();
        Guid nonExistentId = Guid.NewGuid();
        codeFileManager.DeleteFile(nonExistentId);
        Assert.Equal(initialFiles, codeFileManager.GetAllFiles());
    }
    
    [Fact]
    public void DeleteFile_SwitchesToNextFileIfCurrentDeleted()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        codeFileManager.AddFile();
        CodeFile currentFile = codeFileManager.GetCurrentFile();
        CodeFile nextFile = codeFileManager.GetAllFiles().First();

        codeFileManager.DeleteFile(currentFile.Id);

        CodeFile newCurrentFile = codeFileManager.GetCurrentFile();
        Assert.Equal(nextFile.Id, newCurrentFile.Id);
    }
    
    [Fact]
    public void DeleteFile_NoSwitchesIfNotCurrentDeleted()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        codeFileManager.AddFile();
        codeFileManager.AddFile();
        CodeFile firstFile = codeFileManager.GetAllFiles().First();
        codeFileManager.SwitchFile(firstFile.Id);

        CodeFile deleteFile = codeFileManager.GetAllFiles().Last();
        codeFileManager.DeleteFile(deleteFile.Id);

        CodeFile currentFile = codeFileManager.GetCurrentFile();
        Assert.Equal(firstFile.Id, currentFile.Id);
    }
    
    [Fact]
    public void ChangeFileName_ModifiesFileName()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        const string newFileName = "Updated File Name";
        CodeFile fileToModify = codeFileManager.GetAllFiles().First();

        codeFileManager.ChangeFileName(fileToModify.Id, newFileName);

        Assert.Equal(newFileName, fileToModify.Title);
    }
    
    [Fact]
    public void ChangeLanguageOfCurrentFile_ModifiesLanguage()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        const string newLanguage = "C#";

        codeFileManager.ChangeLanguageOfCurrentFile(newLanguage);
        CodeFile currentFile = codeFileManager.GetCurrentFile();

        Assert.Equal(newLanguage, currentFile.Language);
    }
}