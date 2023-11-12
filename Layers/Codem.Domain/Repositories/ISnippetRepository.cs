using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Domain.Common;

namespace Codem.Domain.Repositories;

public interface ISnippetRepository : IRepository<Snippet>
{
    public IEnumerable<Snippet> FindListByTitle(string title);
    public IEnumerable<Snippet> GetAll();
    public void DeleteById(Guid id);
}