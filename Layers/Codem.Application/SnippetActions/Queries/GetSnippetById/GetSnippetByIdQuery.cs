namespace Codem.Application.SnippetActions.Queries.GetSnippetById;

public class GetSnippetByIdQuery : IRequest<SnippetDto>
{
    public Guid Id { get; }

    public GetSnippetByIdQuery(Guid id)
    {
        Id = id;
    }
}