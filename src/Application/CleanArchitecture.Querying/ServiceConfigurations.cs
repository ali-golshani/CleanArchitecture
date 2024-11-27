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
        services.AddTransient(typeof(QueryProcessor<,>));
        services.AddTransient<IQueryService, QueryService>();
        services.RegisterRequestFilters();
        services.RegisterRequestHandlers();
        services.RegisterAccessVerifiers();
        services.RegisterValidators();
    }
}
