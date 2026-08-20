using Framework.Results;
using System.Text.Json.Serialization;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.ChangePassword;

public sealed class Request : RequestBase, IRequest<Request, Empty>
{
    public override string RequestTitle => "Change Password";

    public required string Username { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWriting)]
    public required string OldPassword { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWriting)]
    public required string NewPassword { get; init; }
}
