using Consul;
using Consul.AspNetCore;
using Git2Consul.Api.Configurations;
using Git2Consul.Infrastructure;
using static Git2Consul.Api.DependencyInjection;

namespace Git2Consul.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGit2ConsulEnvironments(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        configuration
            .GetOrThrow<List<Git2ConsulEnvironment>>(Git2ConsulEnvironments)
            .ForEach(git2ConsulEnvironment =>
            {
                serviceCollection.AddConsul(git2ConsulEnvironment.Name, config =>
                {
                    config.Address = new Uri(git2ConsulEnvironment.ConsulAddress);
                    config.Token = git2ConsulEnvironment.ConsulAclToken;
                });
                serviceCollection.AddKeyedTransient<ConsulKeyValueRepository>(
                    git2ConsulEnvironment.Name,
                    (serviceProvider, _) =>
                    {
                        var consulClient = serviceProvider.GetRequiredKeyedService<ConsulClient>(git2ConsulEnvironment.Name);
                        return new ConsulKeyValueRepository(consulClient);
                    });
                serviceCollection.AddKeyedScoped<GitKeyValueRepository>(
                    git2ConsulEnvironment.Name,
                    (serviceProvider, _) => new GitKeyValueRepository(
                        git2ConsulEnvironment.GitRepositoryUrl,
                        git2ConsulEnvironment.Name,
                        serviceProvider.GetRequiredKeyedService<string>(BaseLocalPath)));
            });

        return serviceCollection;
    }
}
