namespace Git2Consul.ApplicationCore.Entities;

public class ConsulEnvironment
{
    public required string Name { get; init; }
    public required string GitRepoUrl { get; init; }
    public required string ConsulAddress { get; init; }
    public required string ConsulAclToken { get; init; }
}
