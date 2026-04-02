using CleanArchitecture.UserManagement.Application.Requests.GetUser;
using CleanArchitecture.UserManagement.Application.Services;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.UserManagement.Endpoints.Users;

internal sealed class GetUser : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapGet("users/{userId:Guid}", Handle)
            .WithTags("Users")
            .RequireAuthorization()
            ;
    }

    private static
        Task<Results<Ok<Application.Requests.Models.User>, NotFound, ProblemHttpResult>>
        Handle(IRequestService requestService, Guid userId, CancellationToken cancellationToken)
    {
        return
            requestService
            .Handle(new Request { UserId = userId }, cancellationToken)
            .ToOkOrNotFoundOrProblem();
    }
}
