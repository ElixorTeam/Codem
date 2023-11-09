using AutoMapper;
using Codem.Domain.Aggregates.SnippetAggregate;
using File=Codem.Domain.Aggregates.SnippetAggregate.File;

namespace Codem.Infrastructure.AutoMapper;

public class InfrastructureMappings : Profile
{
    public InfrastructureMappings()
    {
        CreateMap<Snippet, SqlSnippetEntity>().ReverseMap();
        CreateMap<File, SqlFileEntity>().ReverseMap();
    }
}