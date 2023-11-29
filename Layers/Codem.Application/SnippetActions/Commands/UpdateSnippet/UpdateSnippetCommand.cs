namespace Codem.Application.SnippetActions.Commands.UpdateSnippet;

public class UpdateSnippetCommand : IRequest<SnippetDto>
{
    public SnippetDto Snippet { get; set; }
    
    public UpdateSnippetCommand(SnippetDto snippet)
    {
        Snippet = snippet;
    }
}