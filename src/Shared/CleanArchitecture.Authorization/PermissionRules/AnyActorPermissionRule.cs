namespace CleanArchitecture.Authorization;

public class AnyActorPermissionRule<TCommand> : IPermissionRule<TCommand>
{
    public ValueTask<bool> IsPermit(Actor? actor, TCommand content)
    {
        return ValueTask.FromResult(actor != null);
    }
}
