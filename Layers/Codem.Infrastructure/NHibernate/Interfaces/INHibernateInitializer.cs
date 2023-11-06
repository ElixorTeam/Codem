using NHibernate.Cfg;
namespace Codem.Infrastructure.NHibernate.Interfaces;

public interface INHibernateInitializer
{
    Configuration GetConfiguration();
}