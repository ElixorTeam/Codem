using Codem.Application.Queries.SnippetQueries.GetSnippetById;
using MediatR;
using Сodem.Shared.Dtos.SnippetAggregate;
namespace Codem.Api.Controllers;

public class SnippetController
{
    private readonly ISender _mediator;
    
    public SnippetController(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<SnippetDto> GetSnippetById(Guid id)
    {
        SnippetDto snippetDto = await _mediator.Send(new GetSnippetByIdQuery(id));
        return snippetDto;
    }
}