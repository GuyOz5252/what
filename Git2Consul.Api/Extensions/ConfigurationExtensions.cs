using Git2Consul.Api.Exceptions;

namespace Git2Consul.Api.Extensions;

public static class ConfigurationExtensions
{
    public static T GetOrThrow<T>(this IConfiguration configuration, string key)
    {
        return configuration
            .GetSection(key)
            .Get<T>() ?? throw new ConfigurationException(key);
    }
}
