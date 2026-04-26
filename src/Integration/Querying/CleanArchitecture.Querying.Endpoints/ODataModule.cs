using CleanArchitecture.Querying.Endpoints.Odata;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.Querying.Endpoints;

public sealed class ODataModule : IModule
{
    public ModuleDocument Document { get; } = new()
    {
        Name = "OData",
        Title = "OData",
        Version = "1.0.0",
    };

    public string RoutePrefix => "api/odata/";

    public IEndpointRouteBuilder RouteBuilder(IEndpointRouteBuilder app)
    {
        return
            app
            .MapGroup(RoutePrefix)
            .WithGroupName(Document.Name)
            .WithODataModel(EdmModelBuilder.EdmModel())
            .WithODataOptions(ODataConfigs.OptionsSetup)
            .AddODataQueryEndpointFilter(ODataConfigs.ValidationSetup, ODataConfigs.QuerySetup)
            ;
    }

    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.Map<Orders.GetOrders>();
    }
}
