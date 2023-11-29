using System.Reflection;
using Mapster;
using WebClient.Models;
using WebClient.Utils;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;
using Сodem.Shared.Enums;

namespace Codem.WebClient.Tests;

public class MapsterFixture
{
    public MapsterFixture()
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(WebMapperConfig))!);
    }
}

public class UIMapsterTests : IClassFixture<MapsterFixture>
{
    [Fact]
    public void Map_CodeFile_To_FileDto()
    {
        CodeFileModel codeFileModel = new()
        {
            Text = "Hello World",
            Title = "test_file",
            Language = ProgrammingLanguage.Markdown
        };
        
        FileDto fileDto = codeFileModel.Adapt<FileDto>();
        
        Assert.Equal(codeFileModel.Id, fileDto.Id);
        Assert.Equal(codeFileModel.Title, fileDto.Name);
        Assert.Equal(codeFileModel.Text, fileDto.Data);
    }
    
    [Fact]
    public void Map_CodeFile_To_FileCreateDto()
    {
        CodeFileModel codeFileModel = new()
        {
            Text = "Hello World",
            Title = "test_file",
            Language = ProgrammingLanguage.Markdown
        };

        FileCreateDto fileCreateDto = codeFileModel.Adapt<FileCreateDto>();

        Assert.Equal(codeFileModel.Title, fileCreateDto.Name);
        Assert.Equal(codeFileModel.Text, fileCreateDto.Data);
    }
    
    [Fact]
    public void Map_FileDto_To_CodeFile()
    {
        FileDto fileDto = new()
        {
            Id = Guid.NewGuid(),
            Name = "Test File",
            Data = "Test content"
        };
        
        CodeFileModel codeFileModel = fileDto.Adapt<CodeFileModel>();
        
        Assert.Equal(fileDto.Id, codeFileModel.Id);
        Assert.Equal(fileDto.Name, codeFileModel.Title);
        Assert.Equal(fileDto.Data, codeFileModel.Text);
        Assert.Equal(ProgrammingLanguage.C, codeFileModel.Language);
    }
    
    [Fact]
    public void Map_CodeSnippet_To_SnippetDto()
    {
        CodeFileModel codeFileModel = new()
        {
            Title = "test_file",
            Text = "Test content"
        };

        CodeSnippetModel codeSnippetModel = new()
        {
            Title = "Test Snippet",
            Visibility = SnippetVisibilityEnum.Private,
            Password = "123",
            Files = new() { codeFileModel }
        };

        SnippetDto snippetDto = codeSnippetModel.Adapt<SnippetDto>();

        Assert.NotEqual(Guid.Empty, snippetDto.Id);
        Assert.Equal(codeSnippetModel.Title, snippetDto.Title);
        Assert.Equal(codeSnippetModel.Visibility, snippetDto.Visibility);
        Assert.Equal(codeSnippetModel.Password, snippetDto.Password);
        Assert.Single(snippetDto.Files);
    }
    
    [Fact]
    public void Map_CodeSnippet_To_SnippetCreateDto()
    {
        CodeFileModel codeFileModel = new()
        {
            Title = "test_file",
            Text = "Test content"
        };
        
        CodeSnippetModel codeSnippetModel = new()
        {
            Title = "Test Snippet",
            Visibility = SnippetVisibilityEnum.Public,
            Password = "123",
            Files = new() { codeFileModel }
        };

        SnippetCreateDto snippetCreateDto = codeSnippetModel.Adapt<SnippetCreateDto>();

        Assert.Equal(codeSnippetModel.Title, snippetCreateDto.Title);
        Assert.Equal(codeSnippetModel.Visibility, snippetCreateDto.Visibility);
        Assert.Equal(codeSnippetModel.Password, snippetCreateDto.Password);
        Assert.Single(snippetCreateDto.Files);
    }
    
    [Fact]
    public void Map_SnippetDto_To_CodeSnippet()
    {
        FileDto fileDto = new()
        {
            Id = Guid.NewGuid(),
            Name = "test_file",
            Data = "Test content"
        };

        SnippetDto snippetDto = new()
        {
            Title = "Test Snippet",
            Visibility = SnippetVisibilityEnum.Public,
            Password = "123",
            Files = new() {fileDto}
        };

        CodeSnippetModel codeSnippetModel = snippetDto.Adapt<CodeSnippetModel>();

        Assert.Equal(snippetDto.Title, codeSnippetModel.Title);
        Assert.Equal(snippetDto.Visibility, codeSnippetModel.Visibility);
        Assert.Equal(snippetDto.Password, codeSnippetModel.Password);
        Assert.Equal(snippetDto.ExpireTime, codeSnippetModel.ExpireTime.ToDateTime(TimeOnly.MinValue));
        Assert.Single(codeSnippetModel.Files);
    }
}