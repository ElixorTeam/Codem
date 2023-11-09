using AutoMapper;
using Codem.Domain.Aggregates.SnippetAggregate;
using Сodem.Shared.Dtos.File;
using File=Codem.Domain.Aggregates.SnippetAggregate.File;

namespace Codem.Application.AutoMapper;

public class ApplicationMappings : Profile
{
    public ApplicationMappings()
    {
        CreateMap<SnippetDto, Snippet>().ReverseMap();;
        CreateMap<FileDto, File>().ReverseMap();;
    }
}