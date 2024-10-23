namespace Framework.Persistence.IntegrationEvents;

public class AcquiringDistributedLockException : Exception
{
    internal AcquiringDistributedLockException(string? distributedLockName)
    {
        DistributedLockName = distributedLockName;
    }

    public string? DistributedLockName { get; }

    public override string Message => $"The specified distributed lock is unavailable";
}
