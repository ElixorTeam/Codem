using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;
using UOW.Abstractions;

namespace Codem.Application.SnippetActions.Commands.CreateSnippet;

public class CreateSnippetCommandHandler : IRequestHandler<CreateSnippetCommand, SnippetDto>
{
    private readonly ISnippetRepository _snippetRepository;
    private readonly  IUnitOfWork _unitOfWork;
    
    public CreateSnippetCommandHandler(ISnippetRepository snippetRepository, IUnitOfWork unitOfWork)
    {
        _snippetRepository = snippetRepository;
        _unitOfWork = unitOfWork;
    }
    
    public Task<SnippetDto> Handle(CreateSnippetCommand request, CancellationToken cancellationToken)
    {
        Snippet snippet = new();
        _unitOfWork.ExecuteTransaction(() => {
            snippet = _snippetRepository.Add(request.Snippet.Adapt<Snippet>());
        });
        return Task.FromResult(snippet.Adapt<SnippetDto>());
    }
}