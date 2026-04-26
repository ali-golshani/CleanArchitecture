using CleanArchitecture.UserManagement.Endpoints.Authentication;
using CleanArchitecture.UserManagement.Endpoints.Users;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.UserManagement.Endpoints;

public sealed class UserManagementModule : IModule
{
    public ModuleDocument Document { get; } = new()
    {
        Name = "UserManagement",
        Title = "User Management",
        Version = "1.0.0",
    };

    public string RoutePrefix => "api/";

    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        var authApp = app.MapGroup("auth");

        authApp.Map<LoginByPassword>();
        authApp.Map<LoginByOtp>();
        authApp.Map<RequestSmsOtpForLogin>();
        authApp.Map<RefreshToken>();
        authApp.Map<Logout>();

        var managementApp = app.MapGroup("user-management");

        managementApp.Map<GetUsers>();
        managementApp.Map<GetUser>();
        managementApp.Map<RegisterUser>();
        managementApp.Map<UpdateUser>();
        managementApp.Map<UpdateUserClaims>();
        managementApp.Map<UpdateUserPermissions>();
        managementApp.Map<ChangePassword>();
        managementApp.Map<ResetPassword>();
    }
}
