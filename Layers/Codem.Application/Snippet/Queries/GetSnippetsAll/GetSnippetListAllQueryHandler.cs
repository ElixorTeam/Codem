using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Snippet.Queries.GetSnippetsAll;

public class GetSnippetListAllQueryHandler : IRequestHandler<GetSnippetListAllQuery, List<SnippetDto>>
{
    public Task<List<SnippetDto>>Handle(GetSnippetListAllQuery request, CancellationToken cancellationToken)
    {
        List<SnippetDto> list = new() { new SnippetDto{Name = "new name"} };
        return Task.FromResult(list);
    }
}