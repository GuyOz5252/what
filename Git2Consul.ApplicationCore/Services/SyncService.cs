using Git2Consul.ApplicationCore.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Git2Consul.ApplicationCore.Services;

public class SyncService
{
    public async Task SyncAsync(
        string environmentName,
        [FromKeyedServices("Source")] IKeyValueRepository source,
        [FromKeyedServices("destination")] IKeyValueRepository destination,
        CancellationToken cancellationToken = default)
    {
        var sourceKeyValue = (await source
            .ListAsync(environmentName, cancellationToken))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        var destinationKeyValue = (await destination
            .ListAsync(cancellationToken: cancellationToken))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        var update = sourceKeyValue
            .Where(kv => destinationKeyValue.TryGetValue(kv.Key, out var value) || value != kv.Value)
            .ToList();
        
        var delete = destinationKeyValue.Keys
            .Except(sourceKeyValue.Keys)
            .ToList();
        
        await destination.DeleteAsync(delete, cancellationToken);
        await destination.SetAsync(update, cancellationToken);
    }
}
