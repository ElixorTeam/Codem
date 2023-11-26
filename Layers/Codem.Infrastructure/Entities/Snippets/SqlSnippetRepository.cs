using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;
using NHibernate.Criterion;
using Сodem.Shared.Enums;

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

    public IEnumerable<Snippet> FindPublicListByTitle(string title)
    {
        ICriteria criteria = _session.CreateCriteria<SqlSnippetEntity>();
        
        criteria.Add(Restrictions.InsensitiveLike(nameof(SqlSnippetEntity.Title), title, MatchMode.Anywhere));
        criteria.Add(Restrictions.Eq(nameof(SqlSnippetEntity.Visibility), SnippetVisibilityEnum.Public));
        
        List<SqlSnippetEntity> list = criteria.List<SqlSnippetEntity>().ToList();
        return list.Adapt<List<Snippet>>();
    }
    
    public IEnumerable<Snippet> GetAll()
    {
        ICriteria criteria = _session.CreateCriteria<SqlSnippetEntity>();
        criteria.AddOrder(Order.Desc(nameof(SqlSnippetEntity.CreateDt)));
        
        List<SqlSnippetEntity> list = criteria.List<SqlSnippetEntity>().ToList();
        return list.Adapt<List<Snippet>>();
    }
    
    public IEnumerable<Snippet> GetAllPublic()
    {
        ICriteria criteria = _session.CreateCriteria<SqlSnippetEntity>();
        
        criteria.AddOrder(Order.Desc(nameof(SqlSnippetEntity.CreateDt)));
        criteria.Add(Restrictions.Eq(nameof(SqlSnippetEntity.Visibility), SnippetVisibilityEnum.Public));
        List<SqlSnippetEntity> list = criteria.List<SqlSnippetEntity>().ToList();
        
        return list.Adapt<List<Snippet>>();
    }
    
    public Snippet Add(Snippet snippet)
    {
        SqlSnippetEntity sqlSnippet = snippet.Adapt<SqlSnippetEntity>();
        _session.Save(sqlSnippet);
        return sqlSnippet.Adapt<Snippet>();
    }
    
    public void DeleteById(Guid id)
    {
        SqlSnippetEntity? sqlSnippet = _session.Get<SqlSnippetEntity>(id);
        if (sqlSnippet == null) return;
        _session.Delete(sqlSnippet);
    }
}