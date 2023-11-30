using WebClient.Components.CodeEditor;
using WebClient.Models;
using Сodem.Shared.Enums;

namespace Codem.WebClient.Tests;

public class CodeFileManagerTests
{
    [Fact]
    public void GetAllFiles_ReturnDefaultList()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        
        IList<CodeFileModel> result = codeFileManager.GetAllFiles();
        
        Assert.NotEmpty(result);
    }
    
    [Fact]
    public void GetCurrentFile_ReturnsFirstFile_WhenNoFilesExist()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        
        CodeFileModel result = codeFileManager.GetCurrentFile();
        
        Assert.NotNull(result);
        Assert.Equal("new_file.txt", result.Title);
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

        CodeFileModel addedFileModel = codeFileManager.GetAllFiles().Last();
        Assert.NotNull(addedFileModel);
        Assert.Equal("new_file.txt", addedFileModel.Title);
        Assert.Equal(ProgrammingLanguage.Markdown, addedFileModel.Language);
    }
    
    [Fact]
    public void AddFile_AddsFileWithCustomValues()
    {
        CodeFileManager codeFileManager = new CodeFileManager();

        codeFileManager.AddFile("<h1>Hello World!</h1>", "hello_file.txt", ProgrammingLanguage.Html);

        CodeFileModel addedFileModel = codeFileManager.GetAllFiles().Last();
        Assert.Equal("<h1>Hello World!</h1>", addedFileModel.Text);
        Assert.Equal("hello_file.txt", addedFileModel.Title);
        Assert.Equal(ProgrammingLanguage.Html, addedFileModel.Language);
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
        CodeFileModel fileModelToDelete = codeFileManager.GetAllFiles().First();

        codeFileManager.DeleteFile(fileModelToDelete.Id);

        int updatedCount = codeFileManager.GetAllFiles().Count;
        Assert.Equal(initialCount, updatedCount);
        Assert.DoesNotContain(fileModelToDelete, codeFileManager.GetAllFiles());
    }
    
    [Fact]
    public void DeleteFile_NonExistentFile()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        IList<CodeFileModel> initialFiles = codeFileManager.GetAllFiles();
        Guid nonExistentId = Guid.NewGuid();
        codeFileManager.DeleteFile(nonExistentId);
        Assert.Equal(initialFiles, codeFileManager.GetAllFiles());
    }
    
    [Fact]
    public void DeleteFile_SwitchesToNextFileIfCurrentDeleted()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        codeFileManager.AddFile();
        CodeFileModel currentFileModel = codeFileManager.GetCurrentFile();
        CodeFileModel nextFileModel = codeFileManager.GetAllFiles().First();

        codeFileManager.DeleteFile(currentFileModel.Id);

        CodeFileModel newCurrentFileModel = codeFileManager.GetCurrentFile();
        Assert.Equal(nextFileModel.Id, newCurrentFileModel.Id);
    }
    
    [Fact]
    public void DeleteFile_NoSwitchesIfNotCurrentDeleted()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        codeFileManager.AddFile();
        codeFileManager.AddFile();
        CodeFileModel firstFileModel = codeFileManager.GetAllFiles().First();
        codeFileManager.SwitchFile(firstFileModel.Id);

        CodeFileModel deleteFileModel = codeFileManager.GetAllFiles().Last();
        codeFileManager.DeleteFile(deleteFileModel.Id);

        CodeFileModel currentFileModel = codeFileManager.GetCurrentFile();
        Assert.Equal(firstFileModel.Id, currentFileModel.Id);
    }
    
    [Fact]
    public void ChangeFileName()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        const string newFileName = "updated_file_name.txt";
        CodeFileModel fileModelToModify = codeFileManager.GetAllFiles().First();

        codeFileManager.ChangeFileName(fileModelToModify.Id, newFileName);

        Assert.Equal(newFileName, fileModelToModify.Title);
    }
    
    [Fact]
    public void ChangeLanguageOfCurrentFile_ModifiesLanguage()
    {
        CodeFileManager codeFileManager = new CodeFileManager();
        const ProgrammingLanguage newLanguage = ProgrammingLanguage.CSharp;

        codeFileManager.ChangeLanguageOfCurrentFile(newLanguage);
        CodeFileModel currentFileModel = codeFileManager.GetCurrentFile();

        Assert.Equal(newLanguage, currentFileModel.Language);
    }
}