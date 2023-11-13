using System.Reflection;
using Mapster;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.WebClient.Tests;

public class MapsterFixture
{
    public MapsterFixture()
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(MapperConfig))!);
    }
}

public class UIMapsterTests : IClassFixture<MapsterFixture>
{
    [Fact]
    public void Map_CodeFile_To_FileDto()
    {
        CodeFile codeFile = new CodeFile
        {
            Text = "Hello World",
            Title = "test_file",
            Language = ProgrammingLanguage.Markdown
        };
        
        FileDto fileDto = codeFile.Adapt<FileDto>();
        
        Assert.Equal(codeFile.Id, fileDto.Id);
        Assert.Equal(codeFile.Title, fileDto.Name);
        Assert.Equal(codeFile.Text, fileDto.Data);
    }
    
    [Fact]
    public void Map_CodeFile_To_FileCreateDto()
    {
        CodeFile codeFile = new CodeFile
        {
            Text = "Hello World",
            Title = "test_file",
            Language = ProgrammingLanguage.Markdown
        };

        FileCreateDto fileCreateDto = codeFile.Adapt<FileCreateDto>();

        Assert.Equal(codeFile.Title, fileCreateDto.Name);
        Assert.Equal(codeFile.Text, fileCreateDto.Data);
    }
    
    [Fact]
    public void Map_FileDto_To_CodeFile()
    {
        FileDto fileDto = new FileDto
        {
            Id = Guid.NewGuid(),
            Name = "Test File",
            Data = "Test content"
        };
        
        CodeFile codeFile = fileDto.Adapt<CodeFile>();
        
        Assert.Equal(fileDto.Id, codeFile.Id);
        Assert.Equal(fileDto.Name, codeFile.Title);
        Assert.Equal(fileDto.Data, codeFile.Text);
        Assert.Equal(ProgrammingLanguage.Markdown, codeFile.Language);
    }
    
    [Fact]
    public void Map_CodeSnippet_To_SnippetDto()
    {
        CodeFile codeFile = new CodeFile
        {
            Title = "test_file",
            Text = "Test content"
        };

        CodeSnippet codeSnippet = new CodeSnippet
        {
            Title = "Test Snippet",
            IsPrivate = true,
            Password = "123",
            Files = new List<CodeFile> { codeFile }
        };

        SnippetDto snippetDto = codeSnippet.Adapt<SnippetDto>();

        Assert.NotEqual(Guid.Empty, snippetDto.Id);
        Assert.Equal(codeSnippet.Title, snippetDto.Title);
        Assert.Equal(codeSnippet.IsPrivate, snippetDto.IsPrivate);
        Assert.Equal(codeSnippet.Password, snippetDto.Password);
        Assert.Single(snippetDto.Files);
    }
    
    [Fact]
    public void Map_CodeSnippet_To_SnippetCreateDto()
    {
        CodeFile codeFile = new CodeFile
        {
            Title = "test_file",
            Text = "Test content"
        };
        
        CodeSnippet codeSnippet = new CodeSnippet
        {
            Title = "Test Snippet",
            IsPrivate = true,
            Password = "123",
            Files = new List<CodeFile> { codeFile }
        };

        SnippetCreateDto snippetCreateDto = codeSnippet.Adapt<SnippetCreateDto>();

        Assert.Equal(codeSnippet.Title, snippetCreateDto.Title);
        Assert.Equal(codeSnippet.IsPrivate, snippetCreateDto.IsPrivate);
        Assert.Equal(codeSnippet.Password, snippetCreateDto.Password);
        Assert.Single(snippetCreateDto.Files);
    }
    
    [Fact]
    public void Map_SnippetDto_To_CodeSnippet()
    {
        FileDto fileDto = new FileDto
        {
            Id = Guid.NewGuid(),
            Name = "test_file",
            Data = "Test content"
        };

        SnippetDto snippetDto = new SnippetDto
        {
            Title = "Test Snippet",
            IsPrivate = true,
            Password = "123",
            Files = new List<FileDto> {fileDto}
        };

        CodeSnippet codeSnippet = snippetDto.Adapt<CodeSnippet>();

        Assert.Equal(snippetDto.Title, codeSnippet.Title);
        Assert.Equal(snippetDto.IsPrivate, codeSnippet.IsPrivate);
        Assert.Equal(snippetDto.Password, codeSnippet.Password);
        Assert.Equal(SnippetExpiration.OneWeek, codeSnippet.ExpireTime);
        Assert.Single(codeSnippet.Files);
    }
}