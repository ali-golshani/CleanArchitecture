namespace CleanArchitecture.Mediator;

public interface IDependentCommand
{
    Command ReferenceCommand { get; }
}
