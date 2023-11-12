using UnitOfWork.Abstractions;

namespace Codem.Infrastructure.UnitOfWork;

public class SqlUnitOfWork : IUnitOfWork
{
    private ITransaction? Transaction { get; set; }
    private ISession Session { get; set; }
    
    public SqlUnitOfWork(ISession session)
    {
        Session = session;
        Transaction = Session.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            Transaction?.Commit();
        }
        catch (Exception)
        {
            Rollback();
            throw;
        }
    }

    public void Rollback()
    {
        if (Transaction is { IsActive: true }) 
            Transaction.Rollback();
    }
    
    public void Dispose()
    {
        Transaction?.Dispose();
    }
}