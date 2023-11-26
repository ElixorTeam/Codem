using Codem.Application.SnippetActions.Queries.GetSnippetsAll;
using Codem.Application.SnippetActions.Queries.GetSnippetsPublic;
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
    
    public async Task<List<SnippetDto>> GetSnippetPublicListByName(string name)
    {
        List<SnippetDto> snippetDto = await _mediator.Send(new GetSnippetPublicListByNameQuery(name));
        return snippetDto;
    }
    
    public async Task<List<SnippetDto>> GetSnippetPublicList()
    {
        List<SnippetDto> snippetDtos = await _mediator.Send(new GetSnippetPublicListQuery());
        return snippetDtos;
    }
    
    public async Task<List<SnippetDto>> GetSnippetAllList()
    {
        List<SnippetDto> snippetDtos = await _mediator.Send(new GetSnippetAllListQuery());
        return snippetDtos;
    }
    
    #endregion

    #region DEFAULT
    
    public async Task DeleteSnippet(Guid id)
    {
        await _mediator.Send(new DeleteSnippetCommand(id));
    }

    public async Task<SnippetDto> CreateSnippet(SnippetCreateDto snippet) 
    { 
        SnippetDto snippetDto = await _mediator.Send(new CreateSnippetCommand(snippet));
        return snippetDto;
    }
    
    public async Task UpdateSnippet(SnippetDto snippet) 
    { 
        // await _mediator.Send(new CreateSnippetCommand(snippet));
        
    }

    #endregion
 }