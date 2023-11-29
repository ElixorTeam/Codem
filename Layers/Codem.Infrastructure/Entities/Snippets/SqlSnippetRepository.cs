using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Infrastructure.Entities.UserSnippetFk;
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
        SqlSnippetEntity sqlSnippet = _session.Get<SqlSnippetEntity>(id) ?? new();
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
    
    public IEnumerable<Snippet> GetAllByUser(string userId)
    {
        ICriteria criteria = _session.CreateCriteria<SqlSnippetEntity>()
            .CreateAlias(nameof(SqlSnippetEntity.UserSnippetFk), "user")
            .Add(Restrictions.Eq($"user.{nameof(SqlUserSnippetFkEntity.UserId)}", userId));
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
        if (snippet.UserId != null)
        {
            sqlSnippet.UserSnippetFk = new()
            {
                Snippet = sqlSnippet,
                UserId = snippet.UserId
            };
        }
        _session.Save(sqlSnippet);
        return sqlSnippet.Adapt<Snippet>();
    }
    
    public Snippet Update(Snippet snippet)
    {
        SqlSnippetEntity sqlSnippet = _session.Get<SqlSnippetEntity>(snippet.Id) ?? new();
        
        if (sqlSnippet.Id == Guid.Empty) throw new InvalidDataException();

        SqlSnippetEntity sqlSnippetUpdate = snippet.Adapt<SqlSnippetEntity>();
        sqlSnippetUpdate.CreateDt = sqlSnippet.CreateDt;
        sqlSnippetUpdate.UserSnippetFk = sqlSnippet.UserSnippetFk;
        
        _session.Merge(sqlSnippetUpdate);
        return sqlSnippetUpdate.Adapt<Snippet>();
    }
    
    public void DeleteById(Guid id)
    {
        SqlSnippetEntity? sqlSnippet = _session.Get<SqlSnippetEntity>(id);
        if (sqlSnippet == null) return;
        _session.Delete(sqlSnippet);
    }
}