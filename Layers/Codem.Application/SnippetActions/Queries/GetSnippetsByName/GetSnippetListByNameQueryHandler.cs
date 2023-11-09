namespace Codem.Application.SnippetActions.Queries.GetSnippetsByName;

public class GetSnippetListByNameQueryHandler : IRequestHandler<GetSnippetListByNameQuery, List<SnippetDto>>
{
    
    private readonly ISnippetRepository _snippetRepository;
    
    public GetSnippetListByNameQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }

    public Task<List<SnippetDto>>Handle(GetSnippetListByNameQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<SnippetDto> () { new() { Title = request.Name }});
    }
}