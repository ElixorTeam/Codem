using Codem.Application.SnippetActions.Queries.GetSnippetsAll;
using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;

namespace Codem.Application.SnippetActions.Queries.GetSnippetsPublicAll;

public class GetSnippetListPublicAllQueryHandler : IRequestHandler<GetSnippetListPublicAllQuery, List<SnippetDto>>
{
    private readonly ISnippetRepository _snippetRepository;

    public GetSnippetListPublicAllQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }

    public Task<List<SnippetDto>> Handle(GetSnippetListPublicAllQuery request, CancellationToken cancellationToken)
    {
        List<Snippet> list = _snippetRepository.GetAllPublic().ToList();
        List<SnippetDto> listDto = list.Adapt<List<SnippetDto>>();
        return Task.FromResult(listDto);
    }
}