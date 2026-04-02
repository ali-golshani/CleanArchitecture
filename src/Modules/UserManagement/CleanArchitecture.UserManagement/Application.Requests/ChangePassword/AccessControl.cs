using CleanArchitecture.Authorization;

namespace CleanArchitecture.UserManagement.Application.Requests.ChangePassword;

internal sealed class AccessControl : AccessControlByPermissionRules<Request>
{
    protected override IPermissionRule<Request>[] PermissionRules(Request content)
    {
        return [new PermissionRule()];
    }
}
