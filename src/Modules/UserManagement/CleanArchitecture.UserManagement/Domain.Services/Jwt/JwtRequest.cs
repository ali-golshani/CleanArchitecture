namespace CleanArchitecture.UserManagement.Domain.Services.Jwt;

internal sealed class JwtRequest
{
    public required Guid UserId { get; init; }
    public required string FullName { get; init; }
    public required string Username { get; init; }
    public required IReadOnlyCollection<(string ClaimType, string ClaimValue)> Claims { get; init; }
}
