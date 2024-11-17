using Framework.Exceptions;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class ConnectionStrings
{
    public static string CleanArchitectureConnectionString(this IConfiguration configuration)
    {
        return
            configuration.GetConnectionString("ConnectionString") ??
            throw new ProgrammerException("Connection String is not Set !");
    }
}