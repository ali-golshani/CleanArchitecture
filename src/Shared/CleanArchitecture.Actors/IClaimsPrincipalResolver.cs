using System.Security.Claims;

namespace CleanArchitecture.Actors;

public interface IClaimsPrincipalResolver
{
    ClaimsPrincipal User { get; }
}
