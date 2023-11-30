using Codem.Api.Controllers;
using Codem.Application;
using Codem.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Codem.Api;

public static class DependencyInjection
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddApplication();
        services.AddNhibernate();
        services.AddScoped<SnippetController>();
    }
}