namespace CleanArchitecture.UserManagement.Domain;

internal sealed class UserClaim
{
    public int Id { get; }
    public required Guid UserId { get; init; }
    public required string ClaimType { get; init; }
    public required string ClaimValue { get; init; }
}
