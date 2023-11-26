using Codem.Infrastructure.Common;

namespace Codem.Infrastructure.Entities.Snippets;

public class SqlSnippetEntity : SqlEntity
{
    public virtual string Title { get; set; } = string.Empty;
    public virtual string Password { get; set; } = string.Empty;
    public virtual bool IsVisible { get; set; }
    public virtual IList<SqlFileEntity> Files { get; set; } = new List<SqlFileEntity>();
}