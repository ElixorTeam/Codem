using System.Data;

namespace UOW.Abstractions;

public interface IUnitOfWork : IDisposable
{
    void ExecuteTransaction(Action unitOfWorkAction, IsolationLevel isolationLevel=IsolationLevel.ReadCommitted);
}