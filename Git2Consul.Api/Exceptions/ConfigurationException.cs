namespace Git2Consul.Api.Exceptions;

public class ConfigurationException : Exception
{
    public ConfigurationException(string key) : base($"{key} not configured")
    {
    }
}
