namespace CleanArchitecture.UserManagement.Application.Requests.Models;

public readonly struct Claim
{
    public required string Type { get; init; }
    public required string Value { get; init; }
}