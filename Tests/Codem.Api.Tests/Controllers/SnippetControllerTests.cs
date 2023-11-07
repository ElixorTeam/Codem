using Codem.Api.Controllers;
using Xunit.Abstractions;

using Сodem.Shared.Dtos.SnippetAggregate;

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
       SnippetDto dto = await _controller.GetSnippetById(Guid.Empty);
       _testOutputHelper.WriteLine(dto.Id.ToString());
    }
}