using Mapster;
using WebClient.Models;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;
using Сodem.Shared.Enums;

namespace WebClient.Utils;

public class WebMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CodeFileModel, FileDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Title)
            .Map(dest => dest.Data, src => src.Text);

        config.ForType<FileDto, CodeFileModel>()
            .ConstructUsing(dto => new CodeFileModel
            {
                Id = dto.Id,
                Title = dto.Name,
                Text = dto.Data,
                Language = ProgrammingLanguage.Markdown
            });

        config.ForType<CodeFileModel, FileCreateDto>()
            .Map(dest => dest.Name, src => src.Title)
            .Map(dest => dest.Data, src => src.Text);
        
        config.ForType<CodeSnippetModel, SnippetDto>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.IsPrivate, src => src.IsPrivate)
            .Map(dest => dest.Password, src => string.IsNullOrEmpty(src.Password) ? null : src.Password)
            .Map(dest => dest.Files, src => ConvertToFileDto(src.Files));
        
        config.ForType<CodeSnippetModel, SnippetCreateDto>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.IsPrivate, src => src.IsPrivate)
            .Map(dest => dest.Password, src => string.IsNullOrEmpty(src.Password) ? null : src.Password)
            .Map(dest => dest.Files, src => ConvertToFileCreateDto(src.Files));

        config.ForType<SnippetDto, CodeSnippetModel>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.IsPrivate, src => src.IsPrivate)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.Files, src => ConvertToFileModel(src.Files));
    }

    private static List<FileDto> ConvertToFileDto(IList<CodeFileModel> fileModelList) =>
        fileModelList.Adapt<List<FileDto>>();
    

    private static List<FileCreateDto> ConvertToFileCreateDto(IList<CodeFileModel> fileModelList) =>
        fileModelList.Adapt<List<FileCreateDto>>();
    

    private static List<CodeFileModel> ConvertToFileModel(List<FileDto> fileDtoList) =>
        fileDtoList.Adapt<List<CodeFileModel>>();
}