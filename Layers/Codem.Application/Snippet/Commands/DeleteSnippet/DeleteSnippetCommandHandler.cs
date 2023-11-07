using MediatR;

namespace Codem.Application.Snippet.Commands.DeleteSnippet;

public class DeleteSnippetCommandHandler : IRequestHandler<DeleteSnippetCommand>
{
    public Task Handle(DeleteSnippetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}