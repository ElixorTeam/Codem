using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;

namespace Codem.Application.SnippetActions.Queries.GetSnippetsByName;

public class GetSnippetPublicListByNameQueryHandler : IRequestHandler<GetSnippetPublicListByNameQuery, List<SnippetDto>>
{
    private readonly ISnippetRepository _snippetRepository;

    public GetSnippetPublicListByNameQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }

    public Task<List<SnippetDto>> Handle(GetSnippetPublicListByNameQuery request, CancellationToken cancellationToken)
    {
        List<Snippet> list = _snippetRepository.FindPublicListByTitle(request.Title).ToList();
        List<SnippetDto> listDto = list.Adapt<List<SnippetDto>>();
        return Task.FromResult(listDto);
    }
}