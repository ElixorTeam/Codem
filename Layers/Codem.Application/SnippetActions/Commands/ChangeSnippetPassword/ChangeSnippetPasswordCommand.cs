using Codem.Domain.ValueTypes;

namespace Codem.Application.SnippetActions.Commands.ChangeSnippetPassword;

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