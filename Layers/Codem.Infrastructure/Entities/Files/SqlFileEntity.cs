using Codem.Infrastructure.Common;
using Сodem.Shared.Enums;

namespace Codem.Infrastructure.Entities.Files;

public class SqlFileEntity : SqlEntity
{
    public virtual string Name { get; set; }
    public virtual string Data { get; set; }
    public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }
    public virtual SqlSnippetEntity Snippet { get; set; }
}