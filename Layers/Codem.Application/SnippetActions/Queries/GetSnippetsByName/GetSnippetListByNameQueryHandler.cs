using AutoMapper;
using Codem.Domain.Aggregates.SnippetAggregate;

namespace Codem.Application.SnippetActions.Queries.GetSnippetsByName;

public class GetSnippetListByNameQueryHandler : IRequestHandler<GetSnippetListByNameQuery, List<SnippetDto>>
{
    private readonly IMapper _mapper;
    private readonly ISnippetRepository _snippetRepository;

    public GetSnippetListByNameQueryHandler(ISnippetRepository snippetRepository, IMapper mapper)
    {
        _snippetRepository = snippetRepository;
        _mapper = mapper;
    }

    public Task<List<SnippetDto>> Handle(GetSnippetListByNameQuery request, CancellationToken cancellationToken)
    {
        List<Snippet> list = _snippetRepository.FindListByTitle(request.Title).ToList();
        List<SnippetDto> listDto = _mapper.Map<List<SnippetDto>>(list);
        return Task.FromResult(listDto);
    }
}