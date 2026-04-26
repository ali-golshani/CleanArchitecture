using CleanArchitecture.UserManagement.Application.Requests;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Services;

public interface IRequestService
{
    Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>;
}