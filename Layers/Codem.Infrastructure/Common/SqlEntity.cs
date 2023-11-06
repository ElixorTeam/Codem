namespace SqlCore.Common;

public abstract class SqlEntity : ISqlEntity
{
    public virtual Guid Uid { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual DateTime ChangedDt { get; set; } 
    public virtual string Name { get; set; }

    public SqlEntity()
    {
        Name = string.Empty;
    }
}