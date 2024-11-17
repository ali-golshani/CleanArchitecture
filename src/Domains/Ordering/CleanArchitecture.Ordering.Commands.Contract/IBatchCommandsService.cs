using CleanArchitecture.Mediator.BatchCommands;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands;

public interface IBatchCommandsService<TCommand>
    where TCommand : CommandBase, ICommand<TCommand, Empty>
{
    Task Handle(IReadOnlyCollection<TCommand> commands, BatchCommandHandlingParameters parameters, CancellationToken cancellationToken);
}