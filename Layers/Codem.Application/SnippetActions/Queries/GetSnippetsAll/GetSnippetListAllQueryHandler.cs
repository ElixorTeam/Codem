using AutoMapper;
using Codem.Domain.Aggregates.SnippetAggregate;

namespace Codem.Application.SnippetActions.Queries.GetSnippetsAll;

public class GetSnippetListAllQueryHandler : IRequestHandler<GetSnippetListAllQuery, List<SnippetDto>>
{
    private readonly IMapper _mapper;
    private readonly ISnippetRepository _snippetRepository;

    public GetSnippetListAllQueryHandler(ISnippetRepository snippetRepository, IMapper mapper)
    {
        _snippetRepository = snippetRepository;
        _mapper = mapper;
    }

    public Task<List<SnippetDto>> Handle(GetSnippetListAllQuery request, CancellationToken cancellationToken)
    {
        List<Snippet> list = _snippetRepository.GetAll().ToList();
        List<SnippetDto> listDto = _mapper.Map<List<SnippetDto>>(list);
        return Task.FromResult(listDto);
    }
    
}