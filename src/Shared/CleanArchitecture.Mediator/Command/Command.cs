namespace CleanArchitecture.Mediator;

public abstract class Command : Request
{
    public override bool? ShouldLog => true;
}
