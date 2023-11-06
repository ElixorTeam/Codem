namespace Codem.Infrastructure.NHibernate.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void Commit();
}