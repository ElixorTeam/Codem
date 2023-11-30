using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;
using UOW.Abstractions;

namespace Codem.Application.SnippetActions.Commands.UpdateSnippet;

public class UpdateSnippetCommandHandler : IRequestHandler<UpdateSnippetCommand, SnippetDto>
{
    private readonly ISnippetRepository _snippetRepository;
    private readonly  IUnitOfWork _unitOfWork;
    
    public UpdateSnippetCommandHandler(ISnippetRepository snippetRepository, IUnitOfWork unitOfWork)
    {
        _snippetRepository = snippetRepository;
        _unitOfWork = unitOfWork;
    }
    
    public Task<SnippetDto> Handle(UpdateSnippetCommand request, CancellationToken cancellationToken)
    {
        Snippet snippet = new();
        _unitOfWork.ExecuteTransaction(() => {
            snippet = _snippetRepository.Update(request.Snippet.Adapt<Snippet>());
        });
        return Task.FromResult(snippet.Adapt<SnippetDto>());
    }
}