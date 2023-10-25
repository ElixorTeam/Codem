using System.Data;
using NHibernate;
using NHibernate.Context;
using SqlCore.NHibernate.Interfaces;

namespace SqlCore.NHibernate.UnitOfWork;

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