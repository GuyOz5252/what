using System.Text;
using Consul;
using Git2Consul.ApplicationCore.Abstract;

namespace Git2Consul.Infrastructure;

public class ConsulKeyValueRepository : IKeyValueRepository
{
    private readonly ConsulClient _consulClient;

    public ConsulKeyValueRepository(ConsulClient consulClient)
    {
        _consulClient = consulClient;
    }

    public async Task<string> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        var result = await _consulClient.KV.Get(key, cancellationToken);
        return Encoding.UTF8.GetString(result.Response.Value);
    }

    public Task<List<KeyValuePair<string, string>>> ListAsync(string prefix = "", CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SetAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SetAsync(List<KeyValuePair<string, string>> keyValuePairs, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(List<string> keys, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
