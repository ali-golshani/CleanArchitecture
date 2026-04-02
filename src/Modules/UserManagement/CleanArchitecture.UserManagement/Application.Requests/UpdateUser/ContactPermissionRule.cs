using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.UserManagement.Domain;

namespace CleanArchitecture.UserManagement.Application.Requests.UpdateUser;

internal sealed class ContactPermissionRule(IUserRepository userRepository) : IPermissionRule<Request>
{
    private readonly IUserRepository userRepository = userRepository;

    public async ValueTask<bool> HasPermission(Actor? actor, Request content)
    {
        if (actor is null)
        {
            return false;
        }

        var username = await userRepository.FindUsername(content.UserId);

        if (username == null)
        {
            return false;
        }

        return username == actor.Username;
    }
}