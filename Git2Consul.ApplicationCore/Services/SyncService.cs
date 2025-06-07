using Git2Consul.ApplicationCore.Abstract;

namespace Git2Consul.ApplicationCore.Services;

public class SyncService : ISyncService
{
    public Task SyncAsync(string environmentName, IKeyValueRepository source, IKeyValueRepository destination,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
