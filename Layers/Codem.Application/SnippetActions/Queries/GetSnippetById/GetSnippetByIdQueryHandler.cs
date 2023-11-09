using AutoMapper;
using Codem.Domain.Aggregates.SnippetAggregate;

namespace Codem.Application.SnippetActions.Queries.GetSnippetById;

public class GetSnippetByIdQueryHandler : IRequestHandler<GetSnippetByIdQuery, SnippetDto>
{
    private readonly ISnippetRepository _snippetRepository;
    private readonly IMapper _mapper;
    
    public GetSnippetByIdQueryHandler(ISnippetRepository snippetRepository, IMapper mapper)
    {
        _snippetRepository = snippetRepository;
        _mapper = mapper;
    }

    public Task<SnippetDto> Handle(GetSnippetByIdQuery request, CancellationToken cancellationToken)
    {
        Snippet list = _snippetRepository.FindById(request.Id);
        return Task.FromResult(_mapper.Map<SnippetDto>(list));
    }
}