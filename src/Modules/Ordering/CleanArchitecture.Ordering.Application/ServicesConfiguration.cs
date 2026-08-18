using CleanArchitecture.Ordering.Application.Pipelines;
using CleanArchitecture.Ordering.Application.Services;
using CleanArchitecture.Ordering.Application.AntiCorruption.CommoditySystem;
using CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;
using CleanArchitecture.Ordering.Domain.Services.BusinessRules;
using Framework.Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application;

public static class ServicesConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IQueryService, QueryService>();
        services.AddTransient<ICommandService, CommandService>();
        services.AddTransient(typeof(IBatchCommandsService<>), typeof(BatchCommandsService<>));
        
        services.AddTransient<ICommodityCatalog, CommodityCatalogAcl>();
        services.AddTransient<ICustomerCommodityLicenseVerifier, CustomerCommodityLicenseVerifierAcl>();
 
        services.AddKeyedPipeline<QueryPipeline.Configuration>(typeof(QueryPipeline.Pipeline<,>));
        services.AddKeyedPipeline<CommandPipeline.Configuration>(typeof(CommandPipeline.Pipeline<,>));
    }
}
