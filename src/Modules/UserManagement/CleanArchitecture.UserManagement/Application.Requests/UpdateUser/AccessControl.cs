using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.UserManagement.Domain;

namespace CleanArchitecture.UserManagement.Application.Requests.UpdateUser;

internal sealed class AccessControl(IUserRepository userRepository) : AccessControlByPermissionRules<Request>
{
    private readonly IUserRepository userRepository = userRepository;

    protected override IPermissionRule<Request>[] PermissionRules(Request content)
    {
        var selfPermission = new ContactPermissionRule(userRepository);
        var adminPermission = new RolesPermissionRule<Request>([Role.Administrator, Role.Programmer]);

        return [selfPermission, adminPermission];
    }
}
