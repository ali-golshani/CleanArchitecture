using CleanArchitecture.Authorization.Claims;
using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Domain.Repositories;
using CleanArchitecture.UserManagement.Errors;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.UpdateUserPermissions;

internal sealed class Handler(IUserRepository userRepository) : IRequestHandler<Request, Empty>
{
    private readonly IUserRepository userRepository = userRepository;
    private static readonly StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

    public async Task<Result<Empty>> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Find(request.UserId);

        if (user is null)
        {
            return new UserNotFoundError();
        }

        var requestPermissions = request.Permissions.Distinct().Select(x => x.ToString()).ToList();
        var userClaims = await userRepository.GetUserClaims(request.UserId, UserClaimTypes.Permission);

        foreach (var userClaim in userClaims)
        {
            if (!requestPermissions.Contains(userClaim.ClaimValue, stringComparer))
            {
                userRepository.Remove(userClaim);
            }
        }

        foreach (var permission in requestPermissions)
        {
            if (!userClaims.Select(x => x.ClaimValue).Contains(permission, stringComparer))
            {
                var userClaim = new UserClaim
                {
                    UserId = user.Id,
                    ClaimType = UserClaimTypes.Permission,
                    ClaimValue = permission
                };

                userRepository.Add(userClaim);
            }
        }

        return Empty.Value;
    }
}