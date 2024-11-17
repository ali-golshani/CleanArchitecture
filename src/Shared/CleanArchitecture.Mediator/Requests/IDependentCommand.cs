using CleanArchitecture.Mediator;

namespace Framework.Mediator;

public interface IDependentCommand
{
    Command ReferenceCommand { get; }
}
