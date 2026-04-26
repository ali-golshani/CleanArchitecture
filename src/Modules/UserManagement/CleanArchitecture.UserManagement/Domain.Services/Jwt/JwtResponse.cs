namespace CleanArchitecture.UserManagement.Domain.Services.Jwt;

internal sealed class JwtResponse
{
    public required int LifetimeSeconds { get; init; }
    public required string Token { get; init; }
}