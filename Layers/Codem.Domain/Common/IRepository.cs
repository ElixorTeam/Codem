using Codem.Domain.Aggregates.SnippetAggregate;
namespace Codem.Domain.Common;

public interface IRepository<out TEntity> where TEntity : IEntity
{ 
    Snippet FindById(Guid id);
}