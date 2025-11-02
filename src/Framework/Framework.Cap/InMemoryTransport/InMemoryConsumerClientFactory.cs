using DotNetCore.CAP.Transport;

namespace Framework.Cap.InMemoryTransport;

internal sealed class InMemoryConsumerClientFactory(InMemoryQueue queue) : IConsumerClientFactory
{
    private readonly InMemoryQueue queue = queue;

    public IConsumerClient Create(string groupName, byte groupConcurrent)
    {
        return queue.CreateConsumer(groupName);
    }
}