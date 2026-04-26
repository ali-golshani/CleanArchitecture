using Framework.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Framework.Validation;

public static class DIExtensions
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.RegisterValidators(Assembly.GetCallingAssembly());
    }

    public static void RegisterValidators(this IServiceCollection services, Assembly assembly)
    {
        services.RegisterAsImplementedInterfaces(typeof(FluentValidation.IValidator<>), assembly);
    }

}
