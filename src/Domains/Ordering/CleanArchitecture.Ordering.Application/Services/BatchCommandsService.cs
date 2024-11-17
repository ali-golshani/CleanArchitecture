using CleanArchitecture.Mediator.BatchCommands;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.Services;

internal class BatchCommandsService<TCommand>(ICommandService commandService) :
    BatchCommandsServiceBase<TCommand>,
    IBatchCommandsService<TCommand>
    where TCommand : CommandBase, ICommand<TCommand, Empty>
{
    private readonly ICommandService commandService = commandService;

    protected override async Task<Result<Empty>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        return await commandService.Handle(command, cancellationToken);
    }
}
