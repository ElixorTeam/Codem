using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Domain.ValueTypes;
using Mapster;

namespace Codem.Application.Utils;


// TODO: fix mapster
public class ApplicationMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<SnippetDto, Snippet>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Password,
                src => src.Password != null ? new Password(src.Password) : null)
            .Map(dest => dest.Visibility, src=>src.IsPrivate)
            .Map(dest => dest.Files, src => src.Files.Adapt<IEnumerable<SnippetFile>>());
        
        config.ForType<SnippetCreateDto, Snippet>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Password,
                src => src.Password != null ? new Password(src.Password) : null)
            .Map(dest => dest.Visibility, src=>src.IsPrivate)
            .Map(dest => dest.Files, src => src.Files.Adapt<IEnumerable<SnippetFile>>());
    }
}