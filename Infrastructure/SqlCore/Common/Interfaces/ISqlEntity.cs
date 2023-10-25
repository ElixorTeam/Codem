namespace SqlCore.Common;

public interface ISqlEntity
{
    public Guid Uid { get; set; }
    public DateTime  CreatedAt { get; set; }
    public DateTime  ChangedDt { get; set; }
    public string  Name { get; set; }
}

