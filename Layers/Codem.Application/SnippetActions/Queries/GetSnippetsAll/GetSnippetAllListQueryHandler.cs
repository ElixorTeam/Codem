using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;

namespace Codem.Application.SnippetActions.Queries.GetSnippetsAll;

public class GetSnippetAllListQueryHandler : IRequestHandler<GetSnippetAllListQuery, List<SnippetDto>>
{
    private readonly ISnippetRepository _snippetRepository;

    public GetSnippetAllListQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }

    public Task<List<SnippetDto>> Handle(GetSnippetAllListQuery request, CancellationToken cancellationToken)
    {
        List<Snippet> list = _snippetRepository.GetAll().ToList();
        List<SnippetDto> listDto = list.Adapt<List<SnippetDto>>();
        return Task.FromResult(listDto);
    }
}