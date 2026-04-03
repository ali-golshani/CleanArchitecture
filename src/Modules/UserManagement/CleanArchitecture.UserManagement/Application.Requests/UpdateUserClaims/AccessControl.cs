using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;

namespace CleanArchitecture.UserManagement.Application.Requests.UpdateUserClaims;

internal sealed class AccessControl : AccessControlByPermissionRules<Request>
{
    protected override IPermissionRule<Request>[] PermissionRules(Request content)
    {
        var adminPermission = new RolesPermissionRule<Request>([Role.Administrator, Role.Programmer]);

        return [adminPermission];
    }
}
