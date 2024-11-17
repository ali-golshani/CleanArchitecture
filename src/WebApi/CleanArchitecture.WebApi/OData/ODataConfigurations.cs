using CleanArchitecture.Querying.OrdersQuery;
using CleanArchitecture.WebApi.Shared.OData;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace CleanArchitecture.WebApi.OData;

public static class ODataConfigurations
{
    public static void AddOData(this IMvcBuilder builder)
    {
        builder.AddOData(options =>
        {
            options
            .Select()
            .Filter()
            .OrderBy()
            .Expand()
            .Count()
            .SetMaxTop(ODataConfigs.MaxTop)
            .AddRouteComponents($"odata", EdmModel(), odataServices =>
            {
                odataServices.AddSingleton<IODataQueryValidator, CustomODataQueryValidator>();
            })
            ;

            options.RouteOptions.EnableControllerNameCaseInsensitive = true;
        });
    }

    private static IEdmModel EdmModel()
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
