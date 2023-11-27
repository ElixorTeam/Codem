using Codem.Application.SnippetActions.Queries.GetSnippetById;
using Codem.Application.Utils;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Codem.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetSnippetByIdQueryHandler).Assembly));
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ApplicationMapperConfig).Assembly);
    }
}