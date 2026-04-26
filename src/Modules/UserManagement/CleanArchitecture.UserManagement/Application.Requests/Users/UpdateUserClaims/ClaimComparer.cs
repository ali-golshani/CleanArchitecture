using CleanArchitecture.UserManagement.Application.Requests.Models;
using System.Diagnostics.CodeAnalysis;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.UpdateUserClaims;

internal sealed class ClaimComparer : IEqualityComparer<Claim>
{
    private static readonly StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;

    public bool Equals(Claim x, Claim y)
    {
        return
            x.Type.Equals(y.Type, stringComparison) &&
            x.Value.Equals(y.Value, stringComparison);
    }

    public int GetHashCode([DisallowNull] Claim obj)
    {
        return (obj.Type + obj.Value).GetHashCode();
    }
}
