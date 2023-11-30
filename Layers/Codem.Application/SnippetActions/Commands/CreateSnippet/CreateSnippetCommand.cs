namespace Codem.Application.SnippetActions.Commands.CreateSnippet;

public class CreateSnippetCommand : IRequest<SnippetDto>
{
    public SnippetCreateDto Snippet { get; set; }

    public CreateSnippetCommand(SnippetCreateDto snippet )
    {
        Snippet = snippet;
    }
}