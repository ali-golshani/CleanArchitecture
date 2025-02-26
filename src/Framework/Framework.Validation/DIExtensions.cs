using Framework.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Framework.Validation;

public static class DIExtensions
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        services.RegisterValidators(assembly);
    }

    public static void RegisterValidators(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.RegisterAsImplementedInterfaces(typeof(FluentValidation.IValidator<>), assemblies);
    }

}
