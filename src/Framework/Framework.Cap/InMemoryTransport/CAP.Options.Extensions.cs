namespace Framework.Cap.InMemoryTransport;

public static class CapOptionsExtensions
{
    public static DotNetCore.CAP.CapOptions UseInMemoryMessageQueue(this DotNetCore.CAP.CapOptions options)
    {
        options.RegisterExtension(new InMemoryQueueOptionsExtension());
        return options;
    }
}