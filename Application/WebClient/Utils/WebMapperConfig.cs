using Mapster;
using WebClient.Models;
using Сodem.Shared.Dtos.File;
using Сodem.Shared.Dtos.Snippet;

namespace WebClient.Utils;

public class WebMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CodeFileModel, FileDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Title)
            .Map(dest => dest.Data, src => src.Text)
            .Map(dest => dest.ProgrammingLanguage, src => src.Language);

        config.ForType<FileDto, CodeFileModel>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Name)
            .Map(dest => dest.Text, src => src.Data)
            .Map(dest => dest.Language, src => src.ProgrammingLanguage);

        config.ForType<CodeFileModel, FileCreateDto>()
            .Map(dest => dest.Name, src => src.Title)
            .Map(dest => dest.Data, src => src.Text)
            .Map(dest => dest.ProgrammingLanguage, src => src.Language);
        
        config.ForType<CodeSnippetModel, SnippetDto>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Visibility, src => src.Visibility)
            .Map(dest => dest.Password, src => string.IsNullOrEmpty(src.Password) ? null : src.Password)
            .Map(dest => dest.Files, src => ConvertToFileDto(src.Files))
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.CreateDate, src => src.CreateDate);
        
        config.ForType<CodeSnippetModel, SnippetCreateDto>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Visibility, src => src.Visibility)
            .Map(dest => dest.Password, src => string.IsNullOrEmpty(src.Password) ? null : src.Password)
            .Map(dest => dest.Files, src => ConvertToFileCreateDto(src.Files))
            .Map(dest => dest.UserId, src => src.UserId);;

        config.ForType<SnippetDto, CodeSnippetModel>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Visibility, src => src.Visibility)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.Files, src => ConvertToFileModel(src.Files))
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.CreateDate, src => DateOnly.FromDateTime(src.CreateDate));
    }

    private static List<FileDto> ConvertToFileDto(IList<CodeFileModel> fileModelList) =>
        fileModelList.Adapt<List<FileDto>>();
    

    private static List<FileCreateDto> ConvertToFileCreateDto(IList<CodeFileModel> fileModelList) =>
        fileModelList.Adapt<List<FileCreateDto>>();
    

    private static List<CodeFileModel> ConvertToFileModel(List<FileDto> fileDtoList) =>
        fileDtoList.Adapt<List<CodeFileModel>>();
}