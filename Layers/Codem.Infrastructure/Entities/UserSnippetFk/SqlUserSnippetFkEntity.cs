using Codem.Infrastructure.Common;

namespace Codem.Infrastructure.Entities.UserSnippetFk;

public class SqlUserSnippetFkEntity : SqlEntity
{
    public virtual string UserId { get; set; } = string.Empty;
    public virtual SqlSnippetEntity Snippet { get; set; } = new();
}