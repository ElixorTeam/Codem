using System.Data;
using Codem.Infrastructure.NHibernate.Interfaces;
namespace Codem.Infrastructure.NHibernate.UnitOfWork;

internal class NHibernateUnitOfWorkFactory : IUnitOfWorkFactory
{
    public IUnitOfWork Create(IsolationLevel isolationLevel)
    {
        return new NHibernateUnitOfWork(NHibernateHelper.GetSession().SessionFactory.OpenSession(), isolationLevel);
    }

    public IUnitOfWork Create()
    {
        return Create(IsolationLevel.ReadUncommitted);
    }
    
}