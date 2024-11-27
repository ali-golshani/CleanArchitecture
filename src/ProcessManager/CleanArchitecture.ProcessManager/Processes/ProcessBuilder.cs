using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.ProcessManager.Processors;
using Framework.Exceptions;
using Framework.ProcessManager;
using Framework.ProcessManager.Extensions;

namespace CleanArchitecture.ProcessManager.Processes;

internal static class ProcessBuilder
{
    public static IProcess<TResponse> Process<TRequest, TResponse>(
        this ICommandService commandService,
        Framework.Mediator.Requests.IRequest<TRequest, TResponse> request)
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        return
            commandService
            .AsProcessor<TRequest, TResponse>()
            .Process(request as TRequest ?? throw new ProgrammerException());
    }
}