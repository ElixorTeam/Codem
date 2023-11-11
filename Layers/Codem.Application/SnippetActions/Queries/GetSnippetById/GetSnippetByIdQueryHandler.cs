using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;

namespace Codem.Application.SnippetActions.Queries.GetSnippetById;

public class GetSnippetByIdQueryHandler : IRequestHandler<GetSnippetByIdQuery, SnippetDto>
{
    private readonly ISnippetRepository _snippetRepository;

    public GetSnippetByIdQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }

    public Task<SnippetDto> Handle(GetSnippetByIdQuery request, CancellationToken cancellationToken)
    {
        Snippet item = _snippetRepository.FindById(request.Id);
        return Task.FromResult(item.Adapt<SnippetDto>());
    }
}