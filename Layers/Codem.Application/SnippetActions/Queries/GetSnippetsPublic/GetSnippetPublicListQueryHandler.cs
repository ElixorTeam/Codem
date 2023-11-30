using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;

namespace Codem.Application.SnippetActions.Queries.GetSnippetsPublic;

public class GetSnippetPublicListQueryHandler : IRequestHandler<GetSnippetPublicListQuery, List<SnippetDto>>
{
    private readonly ISnippetRepository _snippetRepository;

    public GetSnippetPublicListQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }

    public Task<List<SnippetDto>> Handle(GetSnippetPublicListQuery request, CancellationToken cancellationToken)
    {
        List<Snippet> list = _snippetRepository.GetAllPublic().ToList();
        List<SnippetDto> listDto = list.Adapt<List<SnippetDto>>();
        return Task.FromResult(listDto);
    }
}