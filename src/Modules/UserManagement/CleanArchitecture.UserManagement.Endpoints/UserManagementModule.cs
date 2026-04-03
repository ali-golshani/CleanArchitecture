using CleanArchitecture.UserManagement.Endpoints.SignOn;
using CleanArchitecture.UserManagement.Endpoints.Users;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.UserManagement.Endpoints;

public sealed class UserManagementModule : IModule
{
    public string Name => "UserManagement";
    public string Title => "User Management";
    public string Version => "1.0.0";
    public string RoutePrefix => "api/user-management/";

    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.Map<Login>();
        app.Map<RefreshToken>();
        app.Map<ChangePassword>();

        app.Map<GetUsers>();
        app.Map<GetUser>();
        app.Map<RegisterUser>();
        app.Map<UpdateUser>();
        app.Map<UpdateUserClaims>();
        app.Map<ResetPassword>();
    }
}
