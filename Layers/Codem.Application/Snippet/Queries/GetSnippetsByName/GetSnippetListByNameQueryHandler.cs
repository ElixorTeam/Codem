using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Snippet.Queries.GetSnippetsByName;

public class GetSnippetListByNameQueryHandler : IRequestHandler<GetSnippetListByNameQuery, List<SnippetDto>>
{
    
    /*private readonly ISnippetRepository _snippetRepository;
    
    public GetSnippetListAllQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }*/

    public Task<List<SnippetDto>>Handle(GetSnippetListByNameQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<SnippetDto> () { new() { Name = request.Name }});
    }
}