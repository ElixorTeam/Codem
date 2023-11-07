using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Snippet.Commands.UpdateSnippet;

public class UpdateSnippetCommandHandler : IRequestHandler<UpdateSnippetCommand, SnippetDto>
{
    public Task<SnippetDto> Handle(UpdateSnippetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}