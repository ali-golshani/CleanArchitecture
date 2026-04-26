using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.Authorization.PermissionRules;
using CleanArchitecture.UserManagement.Domain.Repositories;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.UpdateUser;

internal sealed class AccessControl(IUserRepository userRepository) : AccessControlByPermissionRules<Request>
{
    private readonly IUserRepository userRepository = userRepository;

    protected override IPermissionRule<Request>[] PermissionRules(Request content)
    {
        var contactPermission = new ContactPermissionRule(userRepository);
        var adminPermission = new RolesPermissionRule<Request>([Role.Administrator, Role.Programmer]);

        return [contactPermission, adminPermission];
    }
}
