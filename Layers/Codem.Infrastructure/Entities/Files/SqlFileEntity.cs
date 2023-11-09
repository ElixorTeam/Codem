using Codem.Infrastructure.Common;

namespace Codem.Infrastructure.Entities.Files;

public class SqlFileEntity : SqlEntity
{
    public virtual string Title { get; set; }
    public virtual SqlSnippetEntity Snippet { get; set; }
}