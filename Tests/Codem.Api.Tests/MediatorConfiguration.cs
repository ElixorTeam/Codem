using Codem.Application.Queries.SnippetQueries.GetSnippetById;

using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Codem.Api.Tests;

public static class MediatorConfiguration
{
    public static IMediator Get()
    {
        ServiceCollection services = new();
        services.AddMediatR(typeof(GetSnippetByIdQuery).Assembly);
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IMediator mediator = serviceProvider.GetRequiredService<IMediator>();
        return mediator;
    }
}