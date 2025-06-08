namespace Git2Consul.ApplicationCore.Abstract;

public interface ISyncService
{
    Task SyncAsync(
        string environmentName,
        CancellationToken cancellationToken = default);
}
