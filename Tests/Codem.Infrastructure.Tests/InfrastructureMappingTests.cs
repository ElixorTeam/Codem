using System.Reflection;
using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Infrastructure.Entities.Files;
using Codem.Infrastructure.Entities.Snippets;
using Codem.Infrastructure.Utils;
using Mapster;
using Сodem.Shared.Enums;

namespace Codem.Infrastructure.Tests;

public class MapsterFixture
{
    public MapsterFixture()
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(InfraMapperConfig))!);
    }
}

public class InfrastructureMappingTests: IClassFixture<MapsterFixture>
{
    [Fact]
    public void Map_SnippetFile_To_SqlFileEntity()
    {
        SnippetFile snippetFile = new();
        
        SqlFileEntity sqlFileEntity = snippetFile.Adapt<SqlFileEntity>();
        
        Assert.Equal(snippetFile.Id, sqlFileEntity.Id);
        Assert.Equal(snippetFile.Name, sqlFileEntity.Name);
        Assert.Equal(snippetFile.Data, sqlFileEntity.Data);
        Assert.Equal(snippetFile.ProgrammingLanguage, sqlFileEntity.ProgrammingLanguage);
    }
    
    [Fact]
    public void Map_SqlFileEntity_To_SnippetFile()
    {
        SqlFileEntity sqlFileEntity = new();
        
        SnippetFile snippetFile = sqlFileEntity.Adapt<SnippetFile>();
        
        Assert.Equal(sqlFileEntity.Id, snippetFile.Id);
        Assert.Equal(sqlFileEntity.Name, snippetFile.Name);
        Assert.Equal(sqlFileEntity.Data, snippetFile.Data);
        Assert.Equal(sqlFileEntity.ProgrammingLanguage, snippetFile.ProgrammingLanguage);
    }
    
    [Fact]
    public void Map_Snippet_To_SqlSnippetEntity()
    {
        Snippet snippet = new();
        snippet.AddFile(new());
        
        SqlSnippetEntity sqlSnippetEntity = snippet.Adapt<SqlSnippetEntity>();
        
        Assert.Equal(snippet.Id, sqlSnippetEntity.Id);
        Assert.Equal(snippet.Title, sqlSnippetEntity.Title);
        Assert.Equal(string.Empty, sqlSnippetEntity.Password);
        Assert.Equal(SnippetVisibilityEnum.Public, sqlSnippetEntity.Visibility);
        Assert.Single(sqlSnippetEntity.Files);
        Assert.Equal(sqlSnippetEntity, sqlSnippetEntity.Files.First().Snippet);
    }
    
    [Fact]
    public void Map_SqlSnippetEntity_To_Snippet()
    {
        SqlSnippetEntity sqlSnippetEntity = new();
        
        Snippet snippet = sqlSnippetEntity.Adapt<Snippet>();

        Assert.Equal(sqlSnippetEntity.Id, snippet.Id);
        Assert.Equal(sqlSnippetEntity.Title, snippet.Title);
        Assert.Equal(SnippetVisibilityEnum.Public, snippet.Visibility);
        Assert.Null(snippet.Password);
        Assert.Empty(snippet.Files);
    }
}