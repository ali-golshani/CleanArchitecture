using Asp.Versioning;
using CleanArchitecture.WebApi.Shared.Versioning;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RegisterOrder = CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;

namespace CleanArchitecture.WebApi.Controllers.V3;

[ApiVersion(Versions.V3)]
public class OrderController : BaseController
{
    /// <summary>
    /// Register Order
    /// </summary>
    [HttpPost]
    public
        Task<Results<NoContent, ProblemHttpResult>>
        Add(RegisterOrder.IUseCase useCase, RegisterOrder.Command command, CancellationToken cancellationToken)
    {
        return
            useCase
            .Execute(command, cancellationToken)
            .ToNoContentOrProblem();
    }
}
