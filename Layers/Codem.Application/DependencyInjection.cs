using Codem.Application.Snippet.Queries.GetSnippetById;
using Microsoft.Extensions.DependencyInjection;

namespace Codem.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetSnippetByIdQueryHandler).Assembly));
    }
}