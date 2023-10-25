using NHibernate.Cfg;
namespace SqlCore.NHibernate.Interfaces;

public interface INHibernateInitializer
{
    Configuration GetConfiguration();
}