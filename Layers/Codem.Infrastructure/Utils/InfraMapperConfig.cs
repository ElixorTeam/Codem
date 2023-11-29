using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Domain.ValueTypes;
using Mapster;

namespace Codem.Infrastructure.Utils;

public class InfraMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<SnippetFile, SqlFileEntity>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Data, src => src.Data)
            .Map(dest => dest.ProgrammingLanguage, src => src.ProgrammingLanguage)
            .Ignore(dest => dest.Snippet);
        
        config.ForType<SqlFileEntity, SnippetFile>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Data, src => src.Data)
            .Map(dest => dest.ProgrammingLanguage, src => src.ProgrammingLanguage);

        config.ForType<Snippet, SqlSnippetEntity>()
            .Ignore(dest => dest.CreateDt)
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Password, src => src.Password != null ? src.Password.Value : string.Empty)
            .Map(dest => dest.Visibility, src=>src.Visibility)
            .Map(dest => dest.Files, src => src.Files.Adapt<List<SqlFileEntity>>())
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.ExpireDt, src => src.ExpireDt)
            .AfterMapping((_, dest) => 
            {
                foreach (SqlFileEntity file in dest.Files)
                    file.Snippet = dest;
            });

        config.ForType<SqlSnippetEntity, Snippet>()
            .Ignore(dest => dest.Password!)
            .Ignore(dest => dest.UserId!)
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Visibility, src => src.Visibility)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.ExpireDt, src => src.ExpireDt)
            .Map(dest => dest.CreateDate, src => src.CreateDt)
            .AfterMapping((src, dest) => {
                dest.Password = !string.IsNullOrEmpty(src.Password) ? new Password(src.Password) : null;
                dest.UserId = src.UserSnippetFk?.UserId;
            })
            .Map(dest => dest.Files, src => src.Files.Adapt<List<SnippetFile>>());
    }
}