namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

public interface ISchedulingService
{
    Task Schedule(Request request);
}
