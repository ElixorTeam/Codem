using Codem.Domain.Aggregates.SnippetAggregate;

namespace Codem.Application.SnippetActions.Queries.GetSnippetsAll;

public class GetSnippetListAllQueryHandler : IRequestHandler<GetSnippetListAllQuery, List<SnippetDto>>
{
    private readonly ISnippetRepository _snippetRepository;
    
    public GetSnippetListAllQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }
    
    public Task<List<SnippetDto>>Handle(GetSnippetListAllQuery request, CancellationToken cancellationToken)
    {
        List<Snippet> list = _snippetRepository.GetAll().ToList();
        List<SnippetDto> listDto = list.ConvertAll(SnippetToDto);
        return Task.FromResult(listDto);
    }
    
    private static SnippetDto SnippetToDto(Snippet snippet)
    {
        return new()
        {
            Id = snippet.Id,
            Name = snippet.Name
        };
    }
}