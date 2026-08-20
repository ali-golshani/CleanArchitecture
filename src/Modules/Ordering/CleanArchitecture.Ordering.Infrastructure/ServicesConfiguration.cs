using CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;
using CleanArchitecture.Ordering.Domain.Services.BusinessRules;
using CleanArchitecture.Ordering.Infrastructure.CommoditySystem;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Infrastructure;

public static class ServicesConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<ICommodityCatalog, CommodityCatalogAcl>();
        services.AddTransient<ICustomerCommodityLicenseVerifier, CustomerCommodityLicenseVerifierAcl>();
    }
}
