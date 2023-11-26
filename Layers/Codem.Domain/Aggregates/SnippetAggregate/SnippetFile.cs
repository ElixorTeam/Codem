using Codem.Domain.Common;
using Сodem.Shared.Enums;

namespace Codem.Domain.Aggregates.SnippetAggregate;

public class SnippetFile : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string Data { get; private set; }
    public ProgrammingLanguage ProgrammingLanguage { get; private set; }

    public SnippetFile()
    {
        Name = string.Empty;    
        Data = string.Empty;
        ProgrammingLanguage = ProgrammingLanguage.Markdown;
    }
}