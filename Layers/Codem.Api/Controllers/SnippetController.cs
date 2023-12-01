using Codem.Application.SnippetActions.Commands.UpdateSnippet;
using Codem.Application.SnippetActions.Queries.GetSnippetListByUser;
using Codem.Application.SnippetActions.Queries.GetSnippetsAll;
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
    
    public async Task<List<SnippetDto>> GetSnippetListByUser(string userId)
    {
        List<SnippetDto> snippetDtos = await _mediator.Send(new GetSnippetListByUserQuery(userId));
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
        await _mediator.Send(new UpdateSnippetCommand(snippet));
    }

    #endregion
 }