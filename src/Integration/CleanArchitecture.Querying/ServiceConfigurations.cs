using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares.Extensions;
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
    }
}
