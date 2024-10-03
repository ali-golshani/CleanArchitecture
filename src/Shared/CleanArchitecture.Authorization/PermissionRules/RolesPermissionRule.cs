namespace CleanArchitecture.Authorization;

public class RolesPermissionRule<T>(params Role[] roles) : IPermissionRule<T>
{
    public Role[] Roles { get; } = roles;

    public ValueTask<bool> IsPermit(Actor? actor, T content)
    {
        return ValueTask.FromResult(actor?.Role != null && Roles.Contains(actor.Role));
    }
}
