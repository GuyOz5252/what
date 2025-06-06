using Git2Consul.ApplicationCore.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Git2Consul.ApplicationCore.Services;

public class SyncService
{
    public async Task SyncAsync(
        string environmentName,
        CancellationToken cancellationToken = default)
    {
        var sourceKeyValue = await source
            .ListAsync(environmentName, cancellationToken);
        var destinationKeyValue = await destination
            .ListAsync(cancellationToken: cancellationToken);

        var delete = destinationKeyValue
            .Select(kv => kv.Key)
                .Except(sourceKeyValue.Select(kv => kv.Key))
                .ToList();
        
        await destination.DeleteAsync(delete, cancellationToken);
        await destination.SetAsync(sourceKeyValue, cancellationToken);
    }
}
