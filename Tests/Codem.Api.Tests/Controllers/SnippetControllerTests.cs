using Codem.Api.Controllers;
using Xunit.Abstractions;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Api.Tests.Controllers;


public class SnippetControllerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly SnippetController _controller;
    
    public SnippetControllerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _controller = new(MediatorConfiguration.Get());
    }
    
    [Fact]
    public async void GetSnippetById()
    {
        SnippetDto createDto = await _controller.GetSnippetById(Guid.Empty);
       _testOutputHelper.WriteLine(createDto.Id.ToString());
    }
    
    [Fact]
    public async void GetSnippetListByName()
    {
        const string name = "Test name";
        
        List<SnippetDto> dtos = await _controller.GetSnippetListByName(name);
        
        Assert.NotEmpty(dtos);
    }
}