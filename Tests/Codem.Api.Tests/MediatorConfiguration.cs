using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Codem.Api.Tests;

public static class MediatorConfiguration
{
    public static IMediator Get()
    {
        ServiceCollection services = new();
        services.AddApi();
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IMediator>();
    }
}