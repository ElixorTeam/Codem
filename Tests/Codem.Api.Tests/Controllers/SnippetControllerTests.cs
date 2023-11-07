using Codem.Api.Controllers;

using Сodem.Shared.Dtos.SnippetAggregate;

namespace Codem.Api.Tests.Controllers;


public class SnippetControllerTests
{
    private readonly SnippetController _controller;
    
    public SnippetControllerTests()
    {
        _controller = new(MediatorConfiguration.Get());
    }
    
    [Fact]
    public async void GetSnippetById()
    {
       SnippetDto dto = await _controller.GetSnippetById(Guid.Empty);
    }
}