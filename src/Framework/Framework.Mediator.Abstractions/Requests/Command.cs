namespace Framework.Mediator.Requests;

public abstract class Command : Request
{
    public override bool? ShouldLog => true;
}
