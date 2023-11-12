using Codem.Application.SnippetActions.Queries.GetSnippetById;
using Microsoft.Extensions.DependencyInjection;

namespace Codem.Application.SnippetActions.Queries;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetSnippetByIdQueryHandler).Assembly));
    }
}