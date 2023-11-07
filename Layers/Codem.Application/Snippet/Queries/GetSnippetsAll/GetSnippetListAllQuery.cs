using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Snippet.Queries.GetSnippetsAll;

public class GetSnippetListAllQuery : IRequest<List<SnippetDto>>
{
    public GetSnippetListAllQuery()
    {
    }
}