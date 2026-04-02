namespace CleanArchitecture.UserManagement.Application.Requests.Models;

internal static class Converter
{
    public static User Convert(this Domain.User user, IReadOnlyCollection<Domain.UserClaim> userClaims)
    {
        var claims =
            userClaims.Select(x => new Claim
            {
                Type = x.ClaimType,
                Value = x.ClaimValue
            })
            .ToArray();

        return new User
        {
            Id = user.Id,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            IsActive = user.IsActive,
            Claims = claims
        };
    }
}
