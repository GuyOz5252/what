using Git2Consul.ApplicationCore.Abstract;

namespace Git2Consul.ApplicationCore.Services;

public class SyncService : ISyncService
{
    public Task SyncAsync(string environmentName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
