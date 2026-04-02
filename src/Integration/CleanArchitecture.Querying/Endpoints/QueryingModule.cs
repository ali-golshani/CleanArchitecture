using CleanArchitecture.Querying.Odata;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.Querying.Endpoints;

public sealed class QueryingModule : IModule
{
    public string Name => "Querying";
    public string Title => "Querying";
    public string Version => "1.0.0";
    public string RoutePrefix => "api/querying/";

    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.Map<GetOrders>();
    }

    public IEndpointRouteBuilder RouteBuilder(IEndpointRouteBuilder app)
    {
        return
            app
            .MapGroup(RoutePrefix)
            .WithGroupName(Name)
            .WithODataModel(EdmModelBuilder.EdmModel())
            .AddODataQueryEndpointFilter(ODataConfigs.ValidationSetup, ODataConfigs.QuerySetup)
            .WithODataOptions(ODataConfigs.OptionsSetup)
            ;
    }
}
