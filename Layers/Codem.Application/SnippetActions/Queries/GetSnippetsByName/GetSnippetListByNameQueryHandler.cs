using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;

namespace Codem.Application.SnippetActions.Queries.GetSnippetsByName;

public class GetSnippetListByNameQueryHandler : IRequestHandler<GetSnippetListByNameQuery, List<SnippetDto>>
{
    private readonly ISnippetRepository _snippetRepository;

    public GetSnippetListByNameQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }

    public Task<List<SnippetDto>> Handle(GetSnippetListByNameQuery request, CancellationToken cancellationToken)
    {
        List<Snippet> list = _snippetRepository.FindListByTitle(request.Title).ToList();
        List<SnippetDto> listDto = list.Adapt<List<SnippetDto>>();
        return Task.FromResult(listDto);
    }
}