using CleanArchitecture.UserManagement.Application.Requests.Models;
using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Errors;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.UpdateUserClaims;

internal sealed class Handler(IUserRepository userRepository) : IRequestHandler<Request, Empty>
{
    private readonly IUserRepository userRepository = userRepository;

    public async Task<Result<Empty>> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Find(request.UserId);

        if (user is null)
        {
            return new UserNotFoundError();
        }

        var userClaims = await userRepository.UserClaims(request.UserId);
        var requestClaims = request.Claims.Where(IsValid).Select(Normalize).ToList();

        foreach (var userClaim in userClaims)
        {
            if (!requestClaims.Any(x => Equals(userClaim, x)))
            {
                userRepository.Remove(userClaim);
            }
        }

        foreach (var claim in requestClaims)
        {
            if (!userClaims.Any(x => Equals(x, claim)))
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

    private static bool Equals(UserClaim userClaim, Claim claim)
    {
        return
            userClaim.ClaimType == claim.Type &&
            userClaim.ClaimValue == claim.Value;
    }

    private static bool IsValid(Claim claim)
    {
        return
            !string.IsNullOrWhiteSpace(claim.Type?.Trim()) &&
            !string.IsNullOrWhiteSpace(claim.Value?.Trim());
    }

    private static Claim Normalize(Claim claim)
    {
        return new Claim
        {
            Type = claim.Type.Trim(),
            Value = claim.Value.Trim()
        };
    }
}