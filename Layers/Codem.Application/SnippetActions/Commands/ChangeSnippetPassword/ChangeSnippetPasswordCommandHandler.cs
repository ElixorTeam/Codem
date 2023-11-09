namespace Codem.Application.SnippetActions.Commands.ChangeSnippetPassword;

public class ChangeSnippetPasswordCommandHandler : IRequestHandler<ChangeSnippetPasswordCommand>
{
    public Task Handle(ChangeSnippetPasswordCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Unit.Value);
    }
}