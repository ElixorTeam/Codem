using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Domain.ValueTypes;
using Mapster;

namespace Codem.Application.Utils;


public class ApplicationMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<SnippetDto, Snippet>()
            .Ignore(dest=>dest.Password!)
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Visibility, src=>src.Visibility)
            .Map(dest => dest.UserId, src => src.UserId)
            .AfterMapping((src, dest) => 
            {
                dest.Password = !string.IsNullOrEmpty(src.Password) ? new Password(src.Password) : null;
            })
            .Map(dest => dest.Files, src => src.Files.Adapt<IEnumerable<SnippetFile>>());
        
        config.ForType<SnippetCreateDto, Snippet>()
            .Ignore(dest=>dest.Password!)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Visibility, src=>src.Visibility)
            .Map(dest => dest.UserId, src => src.UserId)
            .AfterMapping((src, dest) => 
            {
                dest.Password = !string.IsNullOrEmpty(src.Password) ? new Password(src.Password) : null;
            })
            .Map(dest => dest.Files, src => src.Files.Adapt<IEnumerable<SnippetFile>>());
    }
}