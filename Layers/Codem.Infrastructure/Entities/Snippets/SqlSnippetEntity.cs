using Codem.Infrastructure.Common;
using Codem.Infrastructure.Entities.UserSnippetFk;
using Сodem.Shared.Enums;

namespace Codem.Infrastructure.Entities.Snippets;

public class SqlSnippetEntity : SqlEntity
{
    public virtual string Title { get; set; } = string.Empty;
    public virtual string UserId { get; set; } = string.Empty;
    public virtual string Password { get; set; } = string.Empty;
    public virtual DateTime ExpireDt { get; set; } = DateTime.MaxValue;
    public virtual SnippetVisibilityEnum Visibility { get; set; } = SnippetVisibilityEnum.Public;
    public virtual IList<SqlFileEntity> Files { get; set; } = new List<SqlFileEntity>();
    public virtual SqlUserSnippetFkEntity? UserSnippetFk { get; set; }
}

