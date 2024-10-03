using Framework.Exceptions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Services;

internal class CommandService(IServiceProvider serviceProvider) : ICommandService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        var useCase = serviceProvider.GetRequiredService<CommandUseCase<TRequest, TResponse>>();
        if (command is not TRequest request) throw new ProgrammerException();
        return useCase.Execute(request, cancellationToken);
    }
}
