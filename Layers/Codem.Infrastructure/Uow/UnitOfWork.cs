using System.Data;
using UOW.Abstractions;

namespace Codem.Infrastructure.Uow;

public class UnitOfWork : IUnitOfWork
{
    private ITransaction? _transaction;
    private readonly ISession _session;
    
    public UnitOfWork(ISession session)
    {
        _session = session;
    }
    
    public void ExecuteTransaction(Action unitOfWorkAction, IsolationLevel isolationLevel)
    {
        if (unitOfWorkAction == null)
            throw new ArgumentNullException(nameof(unitOfWorkAction));

        try
        {
            BeginTransaction(isolationLevel);
            unitOfWorkAction();
            _transaction?.Commit();
            Dispose();
        }
        catch (Exception ex)
        {
            _transaction?.Rollback();
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
    
    private void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (_transaction is { IsActive: true }) return;
        _transaction?.Dispose(); 
        _transaction = _session.BeginTransaction(isolationLevel);
    }
    
    public void Dispose()
    {
        if (_transaction == null) return;
        _transaction.Dispose();
        _transaction = null;
        GC.SuppressFinalize(this);
    }
}
