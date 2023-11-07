using Codem.Domain.ValueTypes;
using MediatR;

namespace Codem.Application.Snippet.Commands.ChangeSnippetPassword;

public class ChangeSnippetPasswordCommand : IRequest
{
    public Guid Id { get; }
    public Password Password { get; }
    
    public ChangeSnippetPasswordCommand(Guid id, string password)
    {
        Id = id;
        Password = new(password);
    }
}