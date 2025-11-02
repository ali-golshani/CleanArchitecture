using DotNetCore.CAP.Transport;

namespace Framework.Cap.InMemoryTransport;

internal sealed class InMemoryConsumerClientFactory(InMemoryQueue queue) : IConsumerClientFactory
{
    public IConsumerClient Create(string groupName, byte groupConcurrent)
    {
        return queue.CreateConsumer(groupName);
    }
}