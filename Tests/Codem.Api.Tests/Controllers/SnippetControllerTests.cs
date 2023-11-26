using Codem.Api.Controllers;
using Xunit.Abstractions;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Api.Tests.Controllers;

public class MediatorFixture
{
    public SnippetController Controller { get; }

    public MediatorFixture()
    {
        Controller = new(MediatorConfiguration.Get());
    }
}

public class SnippetControllerTests : IClassFixture<MediatorFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly SnippetController _controller;

    public SnippetControllerTests(ITestOutputHelper testOutputHelper, MediatorFixture mediatorFixture)
    {
        _testOutputHelper = testOutputHelper;
        _controller = mediatorFixture.Controller;
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
        const string name = "Test";

        List<SnippetDto> dtos = await _controller.GetSnippetListByName(name);

        Assert.NotEmpty(dtos);
    }
}