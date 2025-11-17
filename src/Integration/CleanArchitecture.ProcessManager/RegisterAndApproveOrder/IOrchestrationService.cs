namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

public interface IOrchestrationService
{
    Task<bool> Register(Request request, CancellationToken cancellationToken);
    Task<bool> Approve(Request request, int tryCount, CancellationToken cancellationToken);
    Task<bool> ControlOrderStatus(Request request, CancellationToken cancellationToken);
}
