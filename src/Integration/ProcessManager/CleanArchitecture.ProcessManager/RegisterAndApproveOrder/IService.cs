using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

public interface IService
{
    Task<Result<Empty>> Handle(Request request, CancellationToken cancellationToken);
}
