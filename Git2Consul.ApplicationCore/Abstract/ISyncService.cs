namespace Git2Consul.ApplicationCore.Abstract;

public interface ISyncService
{
    Task SyncAsync(
        string environmentName,
        IKeyValueRepository source,
        IKeyValueRepository destination,
        CancellationToken cancellationToken = default);
}
