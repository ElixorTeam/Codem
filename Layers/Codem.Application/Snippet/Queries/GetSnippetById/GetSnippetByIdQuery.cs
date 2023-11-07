using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Snippet.Queries.GetSnippetById;

public class GetSnippetByIdQuery : IRequest<SnippetDto>
{
    public Guid Id { get; }

    public GetSnippetByIdQuery(Guid id)
    {
        Id = id;
    }
}