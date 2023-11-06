using NHibernate.Mapping.ByCode.Conformist;
using SqlCore.Common;
namespace Codem.Infrastructure.Common;

internal class SqlMap<T> : ClassMapping<T> where T : class, ISqlEntity
{
    protected SqlMap()
    {
        
    }
}