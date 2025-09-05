using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class Service : Pipeline<Request, Empty>, IService
{
    public Service(
        Handler handler,
        ExceptionHandlingMiddleware<Request, Empty> exceptionHandling,
        ValidationMiddleware<Request, Empty> validation)
        : base(handler, exceptionHandling, validation)
    { }
}
