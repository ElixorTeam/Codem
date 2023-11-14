using Codem.Infrastructure.Common;

namespace Codem.Infrastructure.Entities.Snippets;

public class SqlSnippetEntity : SqlEntity
{
    public virtual string Title { get; set; }
    public virtual string Password { get; set; }
    public virtual bool IsVisible { get; set; }
    public virtual IList<SqlFileEntity> Files { get; set; }
}