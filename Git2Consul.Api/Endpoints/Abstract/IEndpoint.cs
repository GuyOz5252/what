namespace Git2Consul.Api.Endpoints.Abstract;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
