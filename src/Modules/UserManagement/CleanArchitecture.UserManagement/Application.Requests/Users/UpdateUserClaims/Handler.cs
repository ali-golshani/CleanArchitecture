using CleanArchitecture.UserManagement.Application.Requests.Models;
using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Domain.Repositories;
using CleanArchitecture.UserManagement.Errors;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.UpdateUserClaims;

internal sealed class Handler(IUserRepository userRepository) : IRequestHandler<Request, Empty>
{
    private readonly IUserRepository userRepository = userRepository;
    private static readonly StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;

    public async Task<Result<Empty>> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Find(request.UserId);

        if (user is null)
        {
            return new UserNotFoundError();
        }

        var userClaims = await userRepository.GetUserClaims(request.UserId);
        var requestClaims = Filtering.Filter(request.Claims);

        foreach (var userClaim in userClaims)
        {
            if (!requestClaims.Any(x => IsEqual(userClaim, x)))
            {
                userRepository.Remove(userClaim);
            }
        }

        foreach (var claim in requestClaims)
        {
            if (!userClaims.Any(x => IsEqual(x, claim)))
            {
                var userClaim = new UserClaim
                {
                    UserId = user.Id,
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                };

                userRepository.Add(userClaim);
            }
        }

        return Empty.Value;
    }

    private static bool IsEqual(UserClaim userClaim, Claim claim)
    {
        return
            userClaim.ClaimType.Equals(claim.Type, stringComparison) &&
            userClaim.ClaimValue.Equals(claim.Value, stringComparison);
    }
}
