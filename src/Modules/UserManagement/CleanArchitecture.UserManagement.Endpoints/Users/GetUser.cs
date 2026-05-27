using CleanArchitecture.UserManagement.Application.Requests.Users.GetUser;
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
        async Task<Results<Ok<Application.Requests.Models.User>, ProblemHttpResult>>
        Handle(IRequestService requestService, Guid userId, CancellationToken cancellationToken)
    {
        var result = await requestService.Handle(new Request { UserId = userId }, cancellationToken);
        return result.ToOkOrNotFoundOrProblem();
    }
}
