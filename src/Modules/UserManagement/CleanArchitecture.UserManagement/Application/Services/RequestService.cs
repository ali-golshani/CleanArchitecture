using CleanArchitecture.Actors;
using CleanArchitecture.UserManagement.Application.Pipelines;
using CleanArchitecture.UserManagement.Application.Requests;
using Framework.Mediator.Extensions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.UserManagement.Application.Services;

internal sealed class RequestService(ActorPreservingScopeFactory scopeFactory) : IRequestService
{
    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        using var scope = scopeFactory.CreateScope();
        var pipeline = scope.ServiceProvider.GetRequiredService<RequestPipeline.Pipeline<TRequest, TResponse>>();
        return await pipeline.Handle(request.AsRequestType(), cancellationToken);
    }
}
