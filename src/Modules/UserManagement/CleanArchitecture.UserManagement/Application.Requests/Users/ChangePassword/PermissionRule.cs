using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.ChangePassword;

internal sealed class PermissionRule : IPermissionRule<Request>
{
    public async ValueTask<bool> HasPermission(Actor? actor, Request content)
    {
        return
            actor is not null &&
            actor.Username == content.Username;
    }
}