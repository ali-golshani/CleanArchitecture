using Framework.Results;
using System.Text.Json.Serialization;

namespace CleanArchitecture.UserManagement.Application.Requests.RegisterUser;

public sealed class Request : RequestBase, IRequest<Request, Empty>
{
    public override string RequestTitle => "Register User";

    public required string Username { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
    public required Models.Role Role { get; init; }
    public required int? BrokerId { get; init;  }
    public required int? CustomerId { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWriting)]
    public required string Password { get; init; }
}
