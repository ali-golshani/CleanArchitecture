using CleanArchitecture.Authorization;
using CleanArchitecture.Querying.Pipelines;
using CleanArchitecture.Querying.Services;
using Framework.Mediator.Extensions;
using Framework.Mediator.Middlewares;
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
        services.RegisterMiddlewares<QueryPipelineConfiguration>();
        services.AddTransient(typeof(IPipeline<,>), typeof(QueryPipeline<,>));

        services.AddTransient(typeof(RequestAuditMiddleware<,>));
    }
}
