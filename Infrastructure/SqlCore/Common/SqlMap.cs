using NHibernate.Mapping.ByCode.Conformist;
namespace SqlCore.Common;

internal class SqlMap<T> : ClassMapping<T> where T : class, ISqlEntity
{
    protected SqlMap()
    {
        
    }
}