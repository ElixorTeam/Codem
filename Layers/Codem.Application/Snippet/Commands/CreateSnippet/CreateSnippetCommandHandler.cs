using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Snippet.Commands.CreateSnippet;

public class CreateSnippetCommandHandler : IRequestHandler<CreateSnippetCommand, SnippetDto>
{
    public Task<SnippetDto> Handle(CreateSnippetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}