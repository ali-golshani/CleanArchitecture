namespace CleanArchitecture.UserManagement.Application.Models;

public readonly struct Claim
{
    public required string Type { get; init; }
    public required string Value { get; init; }
}