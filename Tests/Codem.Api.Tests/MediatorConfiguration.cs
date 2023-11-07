using MediatR;
using Codem.Application.Queries.SnippetQueries.GetSnippetById;
using Microsoft.Extensions.DependencyInjection;

namespace Codem.Api.Tests;

public static class MediatorConfiguration
{
    public static IMediator Get()
    {
        ServiceCollection services = new();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetSnippetByIdQueryHandler).Assembly));
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IMediator>();
    }
}