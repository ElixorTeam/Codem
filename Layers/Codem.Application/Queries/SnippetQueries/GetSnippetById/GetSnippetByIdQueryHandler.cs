using MediatR;
using Сodem.Shared.Dtos.SnippetAggregate;
using Codem.Domain.Repositories;
using Codem.Domain.Aggregates.SnippetAggregate;

namespace Codem.Application.Queries.SnippetQueries.GetSnippetById;

public class GetSnippetByIdQueryHandler : IRequestHandler<GetSnippetByIdQuery, SnippetDto>
{
    
    private readonly ISnippetRepository _snippetRepository;
    
    public GetSnippetByIdQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }

    public Task<SnippetDto> Handle(GetSnippetByIdQuery request, CancellationToken cancellationToken)
    {
        Snippet snippet = _snippetRepository.FindById(request.Id);
        throw new NotImplementedException();
    }
}