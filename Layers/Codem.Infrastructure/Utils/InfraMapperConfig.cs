using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Domain.ValueTypes;
using Mapster;

namespace Codem.Infrastructure.Utils;

public class InfraMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        # region Unnecessary
        
        // config.ForType<SnippetFile, SqlFileEntity>()
        //     .Map(dest => dest.Id, src => src.Id)
        //     .Map(dest => dest.Name, src => src.Name)
        //     .Map(dest => dest.Data, src => src.Data)
        //     .Map(dest => dest.ProgrammingLanguage, src => src.ProgrammingLanguage)
        //     .Ignore(dest => dest.Snippet);
        //
        // config.ForType<SqlFileEntity, SnippetFile>()
        //     .Map(dest => dest.Id, src => src.Id)
        //     .Map(dest => dest.Name, src => src.Name)
        //     .Map(dest => dest.Data, src => src.Data)
        //     .Map(dest => dest.ProgrammingLanguage, src => src.ProgrammingLanguage);
        
        # endregion

        config.ForType<Snippet, SqlSnippetEntity>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Password,
                src => src.Password != null ? src.Password.Value : string.Empty)
            .Map(dest => dest.Visibility, src=>src.Visibility)
            .Map(dest => dest.Files, src => src.Files.Adapt<List<SqlFileEntity>>())
            .AfterMapping((_, dest) => 
        {
            foreach (SqlFileEntity file in dest.Files)
                file.Snippet = dest;
        });

        config.ForType<SqlSnippetEntity, Snippet>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Password, 
                src => string.IsNullOrEmpty(src.Password) ? null : new Password(src.Password))
            .Map(dest => dest.Visibility, src=>src.Visibility)
            .Map(dest => dest.Files, src => src.Files.Adapt<List<SnippetFile>>());
    }
}