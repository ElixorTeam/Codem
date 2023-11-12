using MediatR;
using Сodem.Shared.Dtos.Snippet;

namespace Codem.Api.Controllers;

public class SnippetController
{
    private readonly ISender _mediator;
    
    public SnippetController(ISender mediator)
    {
        _mediator = mediator;
    }

    #region GET

    public async Task<SnippetDto> GetSnippetById(Guid id)
    {
        SnippetDto dto = await _mediator.Send(new GetSnippetByIdQuery(id));
        return dto;
    }
    
    public async Task<List<SnippetDto>> GetSnippetListByName(string name)
    {
        List<SnippetDto> snippetDto = await _mediator.Send(new GetSnippetListByNameQuery(name));
        return snippetDto;
    }
    
    public async Task<List<SnippetDto>> GetSnippetListAll()
    {
        List<SnippetDto> snippetDtos = await _mediator.Send(new GetSnippetListAllQuery());
        return snippetDtos;
    }
    
    #endregion

    #region DEFAULT
    
    public async Task DeleteSnippet(Guid id)
    {
        await _mediator.Send(new DeleteSnippetCommand(id));
    }

    public async Task CreateSnippet(SnippetCreateDto snippet) 
    { 
        await _mediator.Send(new CreateSnippetCommand());
    }
    
    public async Task UpdateSnippet(SnippetDto snippet) 
    { 
        await _mediator.Send(new CreateSnippetCommand());
    }

    #endregion
 }