using Codem.Infrastructure.NHibernate.Interfaces;
using NHibernate;
using NHibernate.Context;
namespace Codem.Infrastructure.NHibernate;

internal static class NHibernateHelper
{
    private static readonly object LockObject = new();
    private static ISessionFactory? SessionFactory { get; set; }
    private static ISessionFactory CreateSessionFactory()
    {
        if (SessionFactory is not null)
            return SessionFactory;
        
        lock (LockObject)
        {
            SessionFactory ??= SqlIoC.Resolve<INHibernateInitializer>().GetConfiguration().BuildSessionFactory();
        }
        
        return SessionFactory;
    }
    public static ISession GetSession()
    {
        ISessionFactory sessionFactory = CreateSessionFactory();
        if (CurrentSessionContext.HasBind(sessionFactory))
            return sessionFactory.GetCurrentSession();
        throw new InvalidOperationException("Database access logic cannot be used, if session not opened.");
    }
}