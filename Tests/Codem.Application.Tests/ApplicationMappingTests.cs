using System.Reflection;
using Codem.Application.Utils;
using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Domain.Enums;
using Mapster;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Tests;

public class MapsterFixture
{
    public MapsterFixture()
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(ApplicationMapperConfig))!);
    }
}

public class ApplicationMappingTests: IClassFixture<MapsterFixture>
{
    [Fact]
    public void Map_SnippetDto_To_Snippet()
    {
        SnippetDto snippetDto = new();

        Snippet snippet = snippetDto.Adapt<Snippet>();
        
        Assert.Equal(snippetDto.Id, snippet.Id);
        Assert.Equal(snippetDto.Title, snippet.Title);
        Assert.Null(snippet.Password);
        Assert.Equal(SnippetVisibilityEnum.Public, snippet.Visibility);
        Assert.Empty(snippet.Files);
    }
    
    [Fact]
    public void Map_SnippetCreateDto_To_Snippet()
    {
        SnippetCreateDto snippetCreateDto = new();

        Snippet snippet = snippetCreateDto.Adapt<Snippet>();
        
        Assert.Equal(snippetCreateDto.Title, snippet.Title);
        Assert.Null(snippet.Password);
        Assert.Equal(SnippetVisibilityEnum.Public, snippet.Visibility);
        Assert.Empty(snippet.Files);
    }
}