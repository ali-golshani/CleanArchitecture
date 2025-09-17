using CleanArchitecture.Querying.GetOrders;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace CleanArchitecture.WebApi.OData.Configs;

internal static class EdmModelBuilder
{
    public static IEdmModel EdmModel()
    {
        var builder = new ODataConventionModelBuilder();
        Configure(builder);
        return builder.GetEdmModel();
    }

    private static void Configure(ODataConventionModelBuilder builder)
    {
        builder.EnableLowerCamelCase();

        builder.EntitySet<Order>("Orders");

        var appointmentConfiguration = builder.EntityType<Order>();
        appointmentConfiguration.HasKey(x => x.OrderId);
    }
}
