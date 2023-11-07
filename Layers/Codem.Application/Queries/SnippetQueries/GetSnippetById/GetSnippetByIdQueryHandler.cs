using MediatR;
using Сodem.Shared.Dtos.SnippetAggregate;

namespace Codem.Application.Queries.SnippetQueries.GetSnippetById;

public class GetSnippetByIdQueryHandler : IRequestHandler<GetSnippetByIdQuery, SnippetDto>
{
    
    /*private readonly ISnippetRepository _snippetRepository;
    
    public GetSnippetByIdQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }*/

    public Task<SnippetDto> Handle(GetSnippetByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SnippetDto() {Name = "Snippet"});
    }
}