namespace Codem.Application.SnippetActions.Commands.UpdateSnippet;

public class UpdateSnippetCommandHandler : IRequestHandler<UpdateSnippetCommand, SnippetDto>
{
    public Task<SnippetDto> Handle(UpdateSnippetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}