using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;
using UOW.Abstractions;

namespace Codem.Application.SnippetActions.Commands.UpdateSnippet;

public class UpdateSnippetCommandHandler : IRequestHandler<UpdateSnippetCommand>
{
    private readonly ISnippetRepository _snippetRepository;
    private readonly  IUnitOfWork _unitOfWork;
    
    public UpdateSnippetCommandHandler(ISnippetRepository snippetRepository, IUnitOfWork unitOfWork)
    {
        _snippetRepository = snippetRepository;
        _unitOfWork = unitOfWork;
    }
    
    public Task Handle(UpdateSnippetCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.ExecuteTransaction(() => {
            _snippetRepository.Update(request.Snippet.Adapt<Snippet>());
        });
        return Task.FromResult(Unit.Value);
    }
}