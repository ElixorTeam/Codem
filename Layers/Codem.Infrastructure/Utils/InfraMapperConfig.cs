using Codem.Domain.Aggregates.SnippetAggregate;
using Mapster;

namespace Codem.Infrastructure.Utils;

public class InfraMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Snippet, SqlSnippetEntity>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title);

        config.ForType<SqlSnippetEntity, Snippet>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title);
    }
}