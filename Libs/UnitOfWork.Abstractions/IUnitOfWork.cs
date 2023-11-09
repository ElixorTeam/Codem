namespace UnitOfWork.Abstractions;

public interface IUnitOfWork : IDisposable
{ 
    void Commit();
    void Rollback();
}