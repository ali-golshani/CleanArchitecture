using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.Authorization.PermissionRules;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.GetUsers;

internal sealed class AccessControl : AccessControlByPermissionRules<Request>
{
    protected override IPermissionRule<Request>[] PermissionRules(Request content)
    {
        var adminPermission = new RolesPermissionRule<Request>([Role.Administrator, Role.Programmer]);

        return [adminPermission];
    }
}
