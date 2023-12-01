using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Domain.Common;

namespace Codem.Domain.Repositories;

public interface ISnippetRepository : IRepository<Snippet>
{
    #region Read

    public IEnumerable<Snippet> GetAll();
    public IEnumerable<Snippet> GetAllByUser(string userId);
    public IEnumerable<Snippet> GetAllPublic();
    public IEnumerable<Snippet> FindPublicListByTitle(string title);
    
    #endregion

    #region CUD

    public void Update(Snippet snippet);
    public Snippet Add(Snippet snippet);
    public void DeleteById(Guid id);
    
    #endregion
}