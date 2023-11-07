using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Snippet.Commands.UpdateSnippet;

public class UpdateSnippetCommand : IRequest<SnippetDto>
{
    public UpdateSnippetCommand()
    {
    }
}