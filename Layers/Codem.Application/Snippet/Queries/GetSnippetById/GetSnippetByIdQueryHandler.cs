using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Snippet.Queries.GetSnippetById;

public class GetSnippetByIdQueryHandler : IRequestHandler<GetSnippetByIdQuery, SnippetDto>
{
    
    /*private readonly ISnippetRepository _snippetRepository;
    
    public GetSnippetListAllQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }*/

    public Task<SnippetDto> Handle(GetSnippetByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SnippetDto() { Name = "Snippet" });
    }
}