using System.Data;
using Codem.Infrastructure.NHibernate.Interfaces;
using NHibernate;
using NHibernate.Context;
namespace Codem.Infrastructure.NHibernate.UnitOfWork;

internal class NHibernateUnitOfWork : IUnitOfWork
{
    private readonly ISession _session;
    private readonly ITransaction _transaction;

    public NHibernateUnitOfWork(ISession session, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        CurrentSessionContext.Bind(session);
        _session = session;
        _transaction = session.BeginTransaction(isolationLevel);
    }

    #region IUnitOfWork Members

    public void Dispose()
    {
        if (_transaction is { WasCommitted: false, WasRolledBack: false })
            _transaction.Rollback();
        
        CurrentSessionContext.Unbind(_session.SessionFactory);
        
        _transaction.Dispose();
        _session.Dispose();
    }

    public void Commit()
    {
        _transaction.Commit();
    }

    #endregion
}