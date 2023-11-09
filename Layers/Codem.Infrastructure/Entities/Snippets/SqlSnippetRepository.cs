using AutoMapper;
using Codem.Domain.Aggregates.SnippetAggregate;
using NHibernate.Criterion;

namespace Codem.Infrastructure.Entities.Snippets;

public class SqlSnippetRepository : ISnippetRepository
{
    private readonly ISession _session;
    private readonly IMapper _mapper;
    
    public SqlSnippetRepository(ISession session, IMapper mapper)
    {
        _session = session;
        _mapper = mapper;
    }

    public Snippet FindById(Guid id)
    {
        SqlSnippetEntity? sqlSnippet = _session.Query<SqlSnippetEntity>().FirstOrDefault(x => x.Id == id);
        return _mapper.Map<Snippet>(sqlSnippet ?? new());
    }

    public IEnumerable<Snippet> FindListByTitle(string title)
    {
        ICriteria criteria = _session.CreateCriteria<SqlSnippetEntity>();
        criteria.Add(Restrictions.InsensitiveLike(nameof(SqlSnippetEntity.Title), title, MatchMode.Anywhere));
        List<SqlSnippetEntity> list = criteria.List<SqlSnippetEntity>().ToList();
        return _mapper.Map<List<Snippet>>(list);
    }
    
    public IEnumerable<Snippet> GetAll()
    {
        List<SqlSnippetEntity> list = _session.Query<SqlSnippetEntity>().ToList();
        return _mapper.Map<List<Snippet>>(list);
    }
}