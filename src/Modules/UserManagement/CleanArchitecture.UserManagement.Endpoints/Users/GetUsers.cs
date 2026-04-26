using CleanArchitecture.UserManagement.Application.Requests.Users.GetUsers;
using CleanArchitecture.UserManagement.Application.Services;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.UserManagement.Endpoints.Users;

internal sealed class GetUsers : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapGet("users", Handle)
            .WithTags("Users")
            .RequireAuthorization()
            ;
    }

    private static
        Task<Results<Ok<IReadOnlyCollection<Application.Requests.Models.User>>, ProblemHttpResult>>
        Handle(IRequestService requestService, [AsParameters] Request request, CancellationToken cancellationToken)
    {
        return
            requestService
            .Handle(request, cancellationToken)
            .ToOkOrProblem();
    }
}
