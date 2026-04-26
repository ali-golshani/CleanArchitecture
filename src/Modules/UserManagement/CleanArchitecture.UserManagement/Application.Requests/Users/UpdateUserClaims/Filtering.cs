using CleanArchitecture.UserManagement.Application.Requests.Models;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.UpdateUserClaims;

internal static class Filtering
{
    public static List<Claim> Filter(IReadOnlyCollection<Claim> claims)
    {
        return
        [..
            claims
            .Where(IsValid)
            .Select(Normalize)
            .Distinct(new ClaimComparer())
        ];
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