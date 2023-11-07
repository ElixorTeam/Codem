using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Snippet.Queries.GetSnippetsAll;

public class GetSnippetListAllQueryHandler : IRequestHandler<GetSnippetListAllQuery, List<SnippetDto>>
{
    public Task<List<SnippetDto>>Handle(GetSnippetListAllQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<SnippetDto> ());
    }
}