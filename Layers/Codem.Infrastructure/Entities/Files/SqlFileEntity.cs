using Codem.Infrastructure.Common;

namespace Codem.Infrastructure.Entities.Files;

public class SqlFileEntity : SqlEntity
{
    public virtual string Name { get; set; }
    public virtual SqlSnippetEntity Snippet { get; set; }
}