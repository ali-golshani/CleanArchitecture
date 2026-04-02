using System.Text.Json.Serialization;

namespace CleanArchitecture.UserManagement.Application.Requests.Login;

public sealed class Request : RequestBase, IRequest<Request, Response>
{
    public override string RequestTitle => "Login";

    public required string Username { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWriting)]
    public required string Password { get; init; }
}