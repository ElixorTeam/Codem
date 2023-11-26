using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Domain.Common;

namespace Codem.Domain.Repositories;

public interface ISnippetRepository : IRepository<Snippet>
{
    #region Read

    public IEnumerable<Snippet> GetAllPublic();
    public IEnumerable<Snippet> FindPublicListByTitle(string title);
    
    #endregion

    #region CUD

    public Snippet Add(Snippet snippet);
    public void DeleteById(Guid id);
    
    #endregion
}