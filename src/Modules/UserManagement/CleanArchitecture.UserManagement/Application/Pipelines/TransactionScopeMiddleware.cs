using CleanArchitecture.UserManagement.Persistence;
using CleanArchitecture.UserManagement.Application.Requests;
using Framework.Mediator.Middlewares;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Pipelines;

internal sealed class TransactionScopeMiddleware<TRequest, TResponse> :
    IMiddleware<TRequest, TResponse>
    where TRequest : RequestBase, IRequest<TRequest, TResponse>
{
    private readonly UserManagementDbContext db;

    public TransactionScopeMiddleware(UserManagementDbContext db)
    {
        this.db = db;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        var cancellationToken = context.CancellationToken;

        var result = await next.Handle(context);

        if (result.IsFailure)
        {
            return result;
        }

        await db.SaveChangesAsync(cancellationToken);

        return result;
    }
}