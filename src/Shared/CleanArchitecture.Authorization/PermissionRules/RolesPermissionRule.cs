namespace CleanArchitecture.Authorization;

public class RolesPermissionRule<T>(params Role[] roles) : IPermissionRule<T>
{
    public Role[] Roles { get; } = roles;

    public ValueTask<bool> HasPermission(Actor? actor, T content)
    {
        return ValueTask.FromResult(actor?.Role != null && Roles.Contains(actor.Role));
    }
}
