using CleanArchitecture.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.ServicesConfigurations;

public static class Extensions
{
    public static DeploymentStage DeploymentStage(this IWebHostEnvironment environment)
    {
        if (environment.IsProduction())
        {
            return Configurations.DeploymentStage.Production;
        }
        else if (environment.IsStaging())
        {
            return Configurations.DeploymentStage.Staging;
        }
        else
        {
            return Configurations.DeploymentStage.Development;
        }
    }
}
