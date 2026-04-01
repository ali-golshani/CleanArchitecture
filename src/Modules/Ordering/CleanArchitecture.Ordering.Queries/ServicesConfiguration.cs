using CleanArchitecture.Authorization;
using Framework.Mediator.Extensions;
using Framework.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Queries;

public static class ServicesConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterRequestHandlers();
        services.RegisterAccessControls();
        services.RegisterFilters();
        services.RegisterValidators();
    }
}
