using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;
using NHibernate.Criterion;

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
        SqlSnippetEntity sqlSnippet = _session.Query<SqlSnippetEntity>().FirstOrDefault(x => x.Id == id) ?? new();
        return sqlSnippet.Adapt<Snippet>();
    }

    public IEnumerable<Snippet> FindListByTitle(string title)
    {
        ICriteria criteria = _session.CreateCriteria<SqlSnippetEntity>();
        criteria.Add(Restrictions.InsensitiveLike(nameof(SqlSnippetEntity.Title), title, MatchMode.Anywhere));
        List<SqlSnippetEntity> list = criteria.List<SqlSnippetEntity>().ToList();
        return list.Adapt<List<Snippet>>();
    }
    
    public IEnumerable<Snippet> GetAll()
    {
        List<SqlSnippetEntity> list = _session.Query<SqlSnippetEntity>().ToList();
        return list.Adapt<List<Snippet>>();
    }
    
    public void DeleteById(Guid id)
    {
        SqlSnippetEntity? sqlSnippet = _session.Get<SqlSnippetEntity>(id);
        if (sqlSnippet == null) return;
        _session.Delete(sqlSnippet);
    }
}