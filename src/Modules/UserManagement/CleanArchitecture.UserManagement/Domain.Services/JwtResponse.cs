namespace CleanArchitecture.UserManagement.Domain.Services;

internal sealed class JwtResponse
{
    public required int Expiry { get; init; }
    public required string Token { get; init; }
}