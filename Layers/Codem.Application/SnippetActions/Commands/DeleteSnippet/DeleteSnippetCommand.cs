namespace Codem.Application.SnippetActions.Commands.DeleteSnippet;

public class DeleteSnippetCommand : IRequest
{
    public Guid Id { get; }

    public DeleteSnippetCommand(Guid id)
    {
        Id = id;
    }
}