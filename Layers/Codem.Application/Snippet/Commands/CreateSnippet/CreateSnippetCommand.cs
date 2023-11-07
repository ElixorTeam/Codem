using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Application.Snippet.Commands.CreateSnippet;

public class CreateSnippetCommand : IRequest<SnippetDto>
{
    public CreateSnippetCommand()
    {
    }
}