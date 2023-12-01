namespace Codem.Application.SnippetActions.Commands.UpdateSnippet;

public class UpdateSnippetCommand : IRequest
{
    public SnippetDto Snippet { get; set; }
    
    public UpdateSnippetCommand(SnippetDto snippet)
    {
        Snippet = snippet;
    }
}