﻿using CleanArchitecture.Ordering.Commands;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.Processes;

internal class OrderingCommandProcessor<TRequest, TResponse> : IRequestProcessor<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    private readonly ICommandService commandService;

    public OrderingCommandProcessor(ICommandService commandService)
    {
        this.commandService = commandService;
    }

    public Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        return commandService.Handle(request, cancellationToken);
    }
}