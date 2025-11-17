namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

public interface IService
{
    Task Schedule(Request request);
}
