namespace Codem.Application.SnippetActions.Queries.GetSnippetsByName;

public class GetSnippetPublicListByNameQuery : IRequest<List<SnippetDto>>
{
    public string Title { get; }

    public GetSnippetPublicListByNameQuery(string title)
    {
        Title = title;
    }
}