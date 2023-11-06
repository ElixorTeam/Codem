using System.Data;
using Codem.Infrastructure.NHibernate.Interfaces;
namespace Codem.Infrastructure.NHibernate.UnitOfWork;

internal interface IUnitOfWorkFactory
{
    IUnitOfWork Create(IsolationLevel isolationLevel);
    
    IUnitOfWork Create();
}