using CleanArchitecture.Authorization;
using Framework.Mediator.Extensions;
using Framework.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Commands;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterNotificationHandlers();
        services.RegisterRequestHandlers();
        services.RegisterAccessControls();
        services.RegisterFilters();
        services.RegisterValidators();
    }
}
