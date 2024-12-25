using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using CleanArchitecture.Querying.Pipeline;
using CleanArchitecture.Querying.Services;
using Framework.Mediator.Extensions;
using Framework.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Querying;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterValidators();
        services.RegisterFilters();
        services.RegisterRequestHandlers();
        services.RegisterAccessControls();

        services.AddTransient<IQueryService, QueryService>();

        services.AddTransient(typeof(QueryPipeline<,>));
        services.AddTransient(typeof(QueryPipelineBuilder<,>));
        services.AddTransient(typeof(RequestAuditMiddleware<,>));

        RegisterQueryMiddlewares(services);
    }

    private static void RegisterQueryMiddlewares(IServiceCollection services)
    {
        var name = Pipelines.Query;

        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(ExceptionHandlingMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(RequestAuditMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(AuthorizationMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(ValidationMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(FilteringMiddleware<,>));
    }
}
