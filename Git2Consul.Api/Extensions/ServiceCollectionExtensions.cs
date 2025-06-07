using Consul;
using Git2Consul.Api.Configurations;
using Git2Consul.Api.Exceptions;
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
                serviceCollection.AddConsulClient<ConsulKeyValueRepository>(
                    git2ConsulEnvironment.Name,
                    config =>
                {
                    config.Address = new Uri(git2ConsulEnvironment.Name);
                    config.Token = git2ConsulEnvironment.ConsulAclToken;
                });
            });

        // Or maybe?
        serviceCollection.Configure<List<Git2ConsulEnvironment>>(configuration.GetSection(Git2ConsulEnvironments));
        
        return serviceCollection;
    }

    private static IServiceCollection AddConsulClient<T>(
        this IServiceCollection serviceCollection,
        string key,
        Action<ConsulClientConfiguration> config)
    where T : IKeyedServiceProvider
    {
        // TODO: Use T
        var client = new ConsulClient(config);
        serviceCollection.AddKeyedSingleton(key, client);
        return serviceCollection;
    }
}
