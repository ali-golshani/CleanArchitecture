namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

public interface IOrchestrationService
{
    Task Register(Request request, CancellationToken cancellationToken);
    Task Approve(Request request, int tryCount, CancellationToken cancellationToken);
    Task ControlOrderStatus(Request request, CancellationToken cancellationToken);
}
