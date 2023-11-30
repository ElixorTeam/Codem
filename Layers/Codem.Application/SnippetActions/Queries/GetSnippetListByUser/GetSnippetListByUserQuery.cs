namespace Codem.Application.SnippetActions.Queries.GetSnippetListByUser;

public class GetSnippetListByUserQuery : IRequest<List<SnippetDto>>
{
    public string UserId { get; init; }
    
    public GetSnippetListByUserQuery(string userId)
    {
        UserId = userId;
    }
}