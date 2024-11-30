using CleanArchitecture.Querying.Services;
using CleanArchitecture.WebApi.Shared.OData;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace CleanArchitecture.WebApi.OData.Controllers;

public class OrdersController(IQueryService queryService) : ODataController
{
    private readonly IQueryService queryService = queryService;

    [HttpGet]
    [EnableODataQuery]
    public Task<ActionResult> Get(Querying.OrdersQuery.Query query, CancellationToken cancellationToken)
    {
        return
            queryService
            .Handle(query, cancellationToken)
            .AsActionResult();
    }
}
