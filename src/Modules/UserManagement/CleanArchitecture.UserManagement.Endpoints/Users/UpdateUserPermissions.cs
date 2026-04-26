using CleanArchitecture.UserManagement.Application.Requests.Users.UpdateUserPermissions;
using CleanArchitecture.UserManagement.Application.Services;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.UserManagement.Endpoints.Users;

internal sealed class UpdateUserPermissions : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapPatch("users/permissions", Handle)
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
