using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;

namespace Codem.Application.SnippetActions.Queries.GetSnippetListByUser;

public class GetSnippetListByUserQueryHandler : IRequestHandler<GetSnippetListByUserQuery, List<SnippetDto>>
{
    private readonly ISnippetRepository _snippetRepository;

    public GetSnippetListByUserQueryHandler(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }

    public Task<List<SnippetDto>> Handle(GetSnippetListByUserQuery request, CancellationToken cancellationToken)
    {
        List<Snippet> list = _snippetRepository.GetAllByUser(request.UserId).ToList();
        List<SnippetDto> listDto = list.Adapt<List<SnippetDto>>();
        return Task.FromResult(listDto);
    }
}