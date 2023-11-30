namespace Codem.Infrastructure.Common;

public abstract class SqlEntity
{
    public virtual Guid Id { get; set; }
    public virtual DateTime  CreateDt { get; set; }
    public virtual DateTime  ChangeDt { get; set; }
}