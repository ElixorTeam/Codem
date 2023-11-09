using Codem.Domain.Aggregates.SnippetAggregate;

namespace Codem.Infrastructure.Entities.Snippets;

public class SqlSnippetRepository : ISnippetRepository
{
    private readonly ISession _session;
    
    public SqlSnippetRepository(ISession session)
    {
        _session = session;
    }

    public Snippet FindById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Snippet> FindListByTitle(string title)
    {
        // ICriteria criteria = _session.CreateCriteria(typeof(SqlSnippetEntity));
        // List<SqlSnippetEntity> list = criteria.List<SqlSnippetEntity>().ToList();
        // return list.ConvertAll(SnippetModelToSnippet);
        throw new NotImplementedException();
    }
    
    public IEnumerable<Snippet> GetAll()
    {
        ICriteria criteria = _session.CreateCriteria(typeof(SqlSnippetEntity));
        List<SqlSnippetEntity> list = criteria.List<SqlSnippetEntity>().ToList();
        return list.ConvertAll(SnippetModelToSnippet);
    }
    
    private static Snippet SnippetModelToSnippet(SqlSnippetEntity snippet)
    {
        return new()
        {
            Id = snippet.Id,
            Name = snippet.Title
        };
    }
}