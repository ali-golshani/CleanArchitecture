using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Mediator.Middlewares.Extensions;

public static class Extensions
{
    public static void RegisterPipelineMiddlewares(
        this IServiceCollection services,
        string pipelineName,
        params Type[] middlewares)
    {
        foreach (var type in middlewares)
        {
            services.AddKeyedTransient(typeof(IMiddleware<,>), pipelineName, type);
        }
    }
}
