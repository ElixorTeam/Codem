using Codem.Domain.Enums;

namespace Codem.Application.SnippetActions.Commands.ChangeSnippetVisibility;

public class ChangeSnippetVisibilityCommand : IRequest
{
    public Guid Id { get; }
    public SnippetVisibilityEnum Visibility { get; }

    public ChangeSnippetVisibilityCommand(Guid id, bool isPrivate)
    {
        Id = id;
        Visibility = isPrivate ? SnippetVisibilityEnum.Private : SnippetVisibilityEnum.Public;
    }
}