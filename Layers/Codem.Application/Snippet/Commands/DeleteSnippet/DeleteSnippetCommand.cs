using MediatR;

namespace Codem.Application.Snippet.Commands.DeleteSnippet;

public class DeleteSnippetCommand : IRequest
{
    public Guid Id { get; }

    public DeleteSnippetCommand(Guid id)
    {
        Id = id;
    }
}