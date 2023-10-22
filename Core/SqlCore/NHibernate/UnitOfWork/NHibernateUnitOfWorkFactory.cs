using System.Data;
using SqlCore.NHibernate.Interfaces;

namespace SqlCore.NHibernate.UnitOfWork;

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