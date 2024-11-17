namespace CleanArchitecture.Mediator.Requests;

public interface IDependentCommand
{
    Command ReferenceCommand { get; }
}
