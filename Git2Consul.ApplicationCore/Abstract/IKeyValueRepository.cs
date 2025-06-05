namespace Git2Consul.ApplicationCore.Abstract;

public interface IKeyValueRepository
{
    Task<string> GetAsync(string key, CancellationToken cancellationToken = default);
    Task<List<KeyValuePair<string, string>>> ListAsync(string prefix = "", CancellationToken cancellationToken = default);
    Task SetAsync(string key, string value, CancellationToken cancellationToken = default);
    Task SetAsync(List<KeyValuePair<string, string>> keyValuePairs, CancellationToken cancellationToken = default);
    Task DeleteAsync(string key, CancellationToken cancellationToken = default);
    Task DeleteAsync(List<string> keys, CancellationToken cancellationToken = default);
}
