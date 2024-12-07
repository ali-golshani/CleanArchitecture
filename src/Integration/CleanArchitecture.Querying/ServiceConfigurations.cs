using CleanArchitecture.Authorization;
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
        services.RegisterQueryFilters();
        services.RegisterDataFilters();
        services.RegisterRequestHandlers();
        services.RegisterAccessVerifiers();

        services.AddTransient(typeof(QueryPipeline<,>));
        services.AddTransient<IQueryService, QueryService>();
        services.AddTransient(typeof(QueryPipelineBuilder<,>));
    }
}
