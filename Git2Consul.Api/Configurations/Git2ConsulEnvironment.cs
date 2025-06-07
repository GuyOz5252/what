namespace Git2Consul.Api.Configurations;

public class Git2ConsulEnvironment
{
    public required string Name { get; init; }
    public required string GitRepositoryUrl { get; init; }
    public required string ConsulAddress { get; init; }
    public required string ConsulAclToken { get; init; }
}
