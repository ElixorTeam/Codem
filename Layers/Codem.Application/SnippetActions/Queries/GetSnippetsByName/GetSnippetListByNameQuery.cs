namespace Codem.Application.SnippetActions.Queries.GetSnippetsByName;

public class GetSnippetListByNameQuery : IRequest<List<SnippetDto>>
{
    public string Name { get; }

    public GetSnippetListByNameQuery(string name)
    {
        Name = name;
    }
}