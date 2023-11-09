namespace Codem.Application.SnippetActions.Queries.GetSnippetsByName;

public class GetSnippetListByNameQuery : IRequest<List<SnippetDto>>
{
    public string Title { get; }

    public GetSnippetListByNameQuery(string title)
    {
        Title = title;
    }
}