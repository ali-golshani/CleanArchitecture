using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal interface IOrchestrationService
{
    Task<Result<Empty>> Register(Request request, CancellationToken cancellationToken);
    Task<Result<Empty>> Approve(Request request, CancellationToken cancellationToken);
    Task<Result<Empty>> ControlOrderStatus(Request request, CancellationToken cancellationToken);
}
