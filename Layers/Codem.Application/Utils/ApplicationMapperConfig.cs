using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Domain.ValueTypes;
using Mapster;

namespace Codem.Application.Utils;

public class ApplicationMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<SnippetDto, Snippet>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title);
        
        config.ForType<SnippetCreateDto, Snippet>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Password, src => src.Password != null ? new Password(src.Password) : null);
    }
}