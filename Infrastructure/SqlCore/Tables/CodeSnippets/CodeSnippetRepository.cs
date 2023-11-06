using Domain.Entities.SnippetAggregate;
namespace SqlCore.Tables.CodeSnippets;

public sealed class CodeSnippetRepository : ISnippetRepository
{
    public Snippet FindById(Guid id)
    {
        throw new NotImplementedException();
    }
}