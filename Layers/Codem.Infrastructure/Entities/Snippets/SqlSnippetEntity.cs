using Codem.Infrastructure.Common;
using Сodem.Shared.Enums;

namespace Codem.Infrastructure.Entities.Snippets;

public class SqlSnippetEntity : SqlEntity
{
    public virtual string Title { get; set; } = string.Empty;
    public virtual string Password { get; set; } = string.Empty;
    public virtual SnippetVisibilityEnum Visibility { get; set; } = SnippetVisibilityEnum.Public;
    public virtual IList<SqlFileEntity> Files { get; set; } = new List<SqlFileEntity>();
}

