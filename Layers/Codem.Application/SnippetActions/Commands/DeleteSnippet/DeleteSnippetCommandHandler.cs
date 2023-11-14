using UOW.Abstractions;

namespace Codem.Application.SnippetActions.Commands.DeleteSnippet;

public class DeleteSnippetCommandHandler : IRequestHandler<DeleteSnippetCommand>
{
    private readonly ISnippetRepository _snippetRepository;
    private readonly  IUnitOfWork _unitOfWork;

    public DeleteSnippetCommandHandler(ISnippetRepository snippetRepository, IUnitOfWork unitOfWork)
    {
        _snippetRepository = snippetRepository;
        _unitOfWork = unitOfWork;
    }

    public Task Handle(DeleteSnippetCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.ExecuteTransaction(() => {
            _snippetRepository.DeleteById(request.Id);
        });
        return Task.FromResult(Unit.Value);
    }
}