using CleanArchitecture.UserManagement.Application.Services;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using CleanArchitecture.UserManagement.Application.Requests.Users.UpdateUser;

namespace CleanArchitecture.UserManagement.Endpoints.Users;

internal sealed class UpdateUser : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapPatch("users", Handle)
            .WithTags("Users")
            .RequireAuthorization()
            ;
    }

    private static
        Task<Results<Ok, ProblemHttpResult>>
        Handle(IRequestService requestService, [FromBody] Request request, CancellationToken cancellationToken)
    {
        return
            requestService
            .Handle(request, cancellationToken)
            .ToOkOrProblem();
    }
}
