using System.Diagnostics;
using System.Text;
using Git2Consul.ApplicationCore.Abstract;

namespace Git2Consul.Infrastructure;

public class GitKeyValueRepository : IKeyValueRepository
{
    private readonly string _gitRepositoryUrl;
    private readonly string _localPath;
    // private readonly string _prefix;

    public GitKeyValueRepository(string gitRepositoryUrl, string prefix, string baseLocalPath)
    {
        _gitRepositoryUrl = gitRepositoryUrl;
        // _prefix = prefix;
        _localPath = $"{baseLocalPath}/{Convert.ToBase64String(Encoding.UTF8.GetBytes(gitRepositoryUrl))}";
    }

    public async Task<string> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        await CloneOrPull(cancellationToken);
        throw new NotImplementedException();
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
    
    private Task CloneOrPull(CancellationToken cancellationToken = default)
    {
        return Directory.Exists(_localPath) ? Pull(cancellationToken) : Clone(cancellationToken);
    }
    
    private async Task Pull(CancellationToken cancellationToken = default)
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "pull origin master",
                WorkingDirectory = _localPath
            }
        };
        
        process.Start();
        await process.WaitForExitAsync(cancellationToken);
        
    }
    
    private async Task Clone(CancellationToken cancellationToken = default)
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = $"clone {_gitRepositoryUrl}",
                WorkingDirectory = _localPath
            }
        };
        
        process.Start();
        await process.WaitForExitAsync(cancellationToken);
    }
}
