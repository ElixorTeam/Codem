namespace Codem.Domain.Common;

public interface IRepository<out TEntity> where TEntity : IEntity
{ 
    TEntity FindById(Guid id);
}