namespace SqlCore.NHibernate.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void Commit();
}