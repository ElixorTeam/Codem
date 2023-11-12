using Codem.Api.Controllers;
using Codem.Application.SnippetActions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Codem.Api;

public static class DependencyInjection
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddApplication();
        services.AddTransient<SnippetController>();
    }
}