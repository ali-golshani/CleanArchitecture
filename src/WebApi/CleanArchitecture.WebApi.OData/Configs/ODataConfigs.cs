using CleanArchitecture.WebApi.Shared.OData;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query.Validator;

namespace CleanArchitecture.WebApi.OData.Configs;

public static class ODataConfigs
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
            .SetMaxTop(Shared.OData.ODataConfigs.MaxTop)
            .AddRouteComponents($"odata", EdmModelBuilder.EdmModel(), odataServices =>
            {
                odataServices.AddSingleton<IODataQueryValidator, CustomODataQueryValidator>();
            })
            ;

            options.RouteOptions.EnableControllerNameCaseInsensitive = true;
        });
    }
}
