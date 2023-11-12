namespace Codem.Application.SnippetActions.Commands.CreateSnippet;

public class CreateSnippetCommandHandler : IRequestHandler<CreateSnippetCommand, SnippetDto>
{
    public Task<SnippetDto> Handle(CreateSnippetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}