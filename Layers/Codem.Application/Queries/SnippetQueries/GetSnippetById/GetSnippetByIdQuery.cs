using MediatR;
using Сodem.Shared.Dtos.SnippetAggregate;
namespace Codem.Application.Queries.SnippetQueries.GetSnippetById;

public class GetSnippetByIdQuery : IRequest<SnippetDto>
{
    public Guid Id { get; }

    public GetSnippetByIdQuery(Guid id)
    {
        Id = id;
    }
}