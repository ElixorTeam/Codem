using System.Data;
using SqlCore.NHibernate.Interfaces;
namespace SqlCore.NHibernate.UnitOfWork;

internal interface IUnitOfWorkFactory
{
    IUnitOfWork Create(IsolationLevel isolationLevel);
    
    IUnitOfWork Create();
}