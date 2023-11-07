using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Snippet.Queries.GetSnippetsByName;

public class GetSnippetListByNameQuery : IRequest<List<SnippetDto>>
{
    public string Name { get; }

    public GetSnippetListByNameQuery(string name)
    {
        Name = name;
    }
}