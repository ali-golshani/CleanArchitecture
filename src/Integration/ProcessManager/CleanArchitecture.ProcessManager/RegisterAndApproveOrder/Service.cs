using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class Service(
    Handler handler,
    ExceptionHandlingMiddleware<Request, Empty> exceptionHandling)
    : Pipeline<Request, Empty>(handler, exceptionHandling), IService
{
    public Task<Result<Empty>> Handle(Request request, CancellationToken cancellationToken)
    {
        return base.Handle(new RequestContext<Request>
        {
            Request = request,
            CancellationToken = cancellationToken,
            ExecutionStartTime = DateTime.Now,
        });
    }
}
