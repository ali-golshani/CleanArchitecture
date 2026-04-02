namespace Framework.WebApi;

public interface IMinimalEndpoint
{
    static abstract void AddRoute(IEndpointRouteBuilder app);
}
