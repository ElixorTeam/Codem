using MediatR;

namespace Codem.Application.Snippet.Commands.ChangeSnippetVisibility;

public class ChangeSnippetVisibilityCommandHandler : IRequestHandler<ChangeSnippetVisibilityCommand>
{
    public Task Handle(ChangeSnippetVisibilityCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Unit.Value);
    }
}