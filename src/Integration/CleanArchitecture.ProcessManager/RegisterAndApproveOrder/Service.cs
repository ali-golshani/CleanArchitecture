using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class Service(
    Handler handler,
    ExceptionHandlingMiddleware<Request, Empty> exceptionHandling)
    : Pipeline<Request, Empty>(handler, exceptionHandling), IService
{
    public async Task Schedule(Request request)
    {
        await Handle(request, cancellationToken: default);
    }
}