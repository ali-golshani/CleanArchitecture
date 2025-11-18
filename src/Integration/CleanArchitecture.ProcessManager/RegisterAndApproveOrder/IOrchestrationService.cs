using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

public interface IOrchestrationService
{
    Task<SerializableResult<Empty>> Register(Request request, CancellationToken cancellationToken);
    Task<SerializableResult<Empty>> Approve(Request request, int tryCount, CancellationToken cancellationToken);
    Task<SerializableResult<Empty>> ControlOrderStatus(Request request, CancellationToken cancellationToken);
}
