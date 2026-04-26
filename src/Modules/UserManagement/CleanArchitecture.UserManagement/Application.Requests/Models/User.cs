namespace CleanArchitecture.UserManagement.Application.Requests.Models;

public sealed class User
{
    public required Guid Id { get; init; }
    public required string Username { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string? PhoneNumber { get; init; }
    public required bool IsActive { get; init; }
    public required Claim[] Claims { get; init; }

    public string Name => $"{FirstName} {LastName}";
}
