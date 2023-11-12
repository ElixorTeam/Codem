namespace Codem.Application.SnippetActions.Commands.DeleteSnippet;

public class DeleteSnippetCommandHandler : IRequestHandler<DeleteSnippetCommand>
{
    private readonly ISnippetRepository _snippetRepository;

    public DeleteSnippetCommandHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }

    public Task Handle(DeleteSnippetCommand request, CancellationToken cancellationToken)
    {
        _snippetRepository.DeleteById(request.Id);
        return Task.FromResult(Unit.Value);
    }
}