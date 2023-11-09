using Mapster;
using WebClient.Models;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Utils;

public class MapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CodeFile, FileDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Title)
            .Map(dest => dest.Data, src => src.Text);

        config.ForType<FileDto, CodeFile>()
            .ConstructUsing(dto => new CodeFile
            {
                Title = dto.Name,
                Text = dto.Data,
                Language = "Markdown"
            });

        config.ForType<CodeFile, FileCreateDto>()
            .Map(dest => dest.Name, src => src.Title)
            .Map(dest => dest.Data, src => src.Text);
        
        config.ForType<CodeSnippet, SnippetDto>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.IsPrivate, src => src.IsPrivate)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.Files, src => ConvertToFileDto(src.Files));
        
        config.ForType<CodeSnippet, SnippetCreateDto>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.IsPrivate, src => src.IsPrivate)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.Files, src => ConvertToFileCreateDto(src.Files));

        config.ForType<SnippetDto, CodeSnippet>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.IsPrivate, src => src.IsPrivate)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.Files, src => ConvertToFileModel(src.Files))
            .ConstructUsing((dto, context) => new CodeSnippet
            {
                ExpireTime = new TimeSpan(1, 0, 0)
            });

    }

    private List<FileDto> ConvertToFileDto(IList<CodeFile> fileModelList)
    {
        return fileModelList.Adapt<List<FileDto>>();
    }

    private List<FileCreateDto> ConvertToFileCreateDto(IList<CodeFile> fileModelList)
    {
        return fileModelList.Adapt<List<FileCreateDto>>();
    }

    private List<CodeFile> ConvertToFileModel(List<FileDto> fileDtoList)
    {
        return fileDtoList.Adapt<List<CodeFile>>();
    }
}