using Codem.Domain.Aggregates.SnippetAggregate;
using Codem.Domain.ValueTypes;
using Mapster;
using Сodem.Shared.Dtos.File;

namespace Codem.Application.Utils;


public class ApplicationMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        #region SnippetDto, Snippet

        config.ForType<SnippetDto, Snippet>()
            .Ignore(dest=>dest.Password!)
            .Ignore(dest=>dest.CreateDate)
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Visibility, src=>src.Visibility)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.ExpireDt, src => src.ExpireTime)
            .AfterMapping((src, dest) => 
            {
                dest.Password = !string.IsNullOrEmpty(src.Password) ? new Password(src.Password) : null;
            })
            .Map(dest => dest.Files, src => src.Files.Adapt<IEnumerable<SnippetFile>>());
        
        config.ForType<Snippet, SnippetDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Visibility, src=>src.Visibility)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.CreateDate, src => src.CreateDate)
            .Map(dest => dest.ExpireTime, src => src.ExpireDt)
            .Map(dest => dest.Files, src => src.Files.Adapt<IEnumerable<FileDto>>());
        
        config.ForType<SnippetCreateDto, Snippet>()
            .Ignore(dest=>dest.CreateDate)
            .Ignore(dest => dest.Password!)
            .Ignore(dest => dest.CreateDate)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Visibility, src=>src.Visibility)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.ExpireDt, src => src.ExpireTime)
            .AfterMapping((src, dest) => 
            {
                dest.Password = !string.IsNullOrEmpty(src.Password) ? new Password(src.Password) : null;
            })
            .Map(dest => dest.Files, src => src.Files.Adapt<IEnumerable<SnippetFile>>());

        #endregion
        
        #region FileDto, File

        config.ForType<FileDto, SnippetFile>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.ProgrammingLanguage, src => src.ProgrammingLanguage)
            .Map(dest => dest.Data, src => src.Data)
            .Map(dest => dest.Name, src => src.Name);
        
        config.ForType<SnippetFile, FileDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.ProgrammingLanguage, src => src.ProgrammingLanguage)
            .Map(dest => dest.Data, src => src.Data)
            .Map(dest => dest.Name, src => src.Name);
        
        config.ForType<FileCreateDto, SnippetFile>()
            .Map(dest => dest.ProgrammingLanguage, src => src.ProgrammingLanguage)
            .Map(dest => dest.Data, src => src.Data)
            .Map(dest => dest.Name, src => src.Name);

        #endregion
    }
}