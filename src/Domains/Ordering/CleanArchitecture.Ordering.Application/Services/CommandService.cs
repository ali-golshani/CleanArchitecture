using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Application.RequestProcessors;
using Framework.Exceptions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Services;

internal class CommandService(IServiceProvider serviceProvider) : ICommandService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        var processor = serviceProvider.GetRequiredService<CommandProcessor<TRequest, TResponse>>();
        if (command is not TRequest request) throw new ProgrammerException();
        return processor.Handle(request, cancellationToken);
    }

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        var processor = serviceProvider.GetRequiredService<CommandProcessor<TRequest, TResponse>>();
        if (command is not TRequest request) throw new ProgrammerException();
        return processor.Handle(actor, request, cancellationToken);
    }
}
