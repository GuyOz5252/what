using Git2Consul.Api.Endpoints.Abstract;
using Git2Consul.ApplicationCore.Abstract;

namespace Git2Consul.Api.Endpoints;

public class SyncEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("sync", async (
                string environmentName,
                IServiceProvider serviceProvider,
                ISyncService syncService,
                CancellationToken cancellationToken) =>
            {
                await syncService.SyncAsync(
                    environmentName,
                    serviceProvider.GetRequiredKeyedService<IKeyValueRepository>($"{environmentName}-Git"),
                    serviceProvider.GetRequiredKeyedService<IKeyValueRepository>($"{environmentName}-Consul"),
                    cancellationToken);
                return Results.Ok();
            })
            .WithTags(Tags.Git2Consul);
    }
}
