using Consul;
using Consul.AspNetCore;
using Git2Consul.Api.Configurations;
using Git2Consul.Api.Exceptions;
using Git2Consul.ApplicationCore.Abstract;
using Git2Consul.Infrastructure;

namespace Git2Consul.Api.Extensions;

public static class ServiceCollectionExtensions
{
    private const string Git2ConsulEnvironments = "Git2ConsulEnvironments";

    public static IServiceCollection AddGit2ConsulEnvironments(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        (configuration
                .GetSection(Git2ConsulEnvironments)
                .Get<List<Git2ConsulEnvironment>>() ?? throw new ConfigurationException(Git2ConsulEnvironments))
            .ForEach(git2ConsulEnvironment =>
            {
                serviceCollection.AddConsul(git2ConsulEnvironment.Name, config =>
                {
                    config.Address = new Uri(git2ConsulEnvironment.GitRepositoryUrl);
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
                        serviceProvider.GetRequiredKeyedService<string>("BaseLocalPath")));
            });

        return serviceCollection;
    }
}
