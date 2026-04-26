using DotNetCore.CAP.Transport;

namespace Framework.Cap.InMemoryTransport;

internal sealed class InMemoryConsumerClientFactory(InMemoryQueue queue) : IConsumerClientFactory
{
    private readonly InMemoryQueue queue = queue;

    public Task<IConsumerClient> CreateAsync(string groupName, byte groupConcurrent)
    {
        IConsumerClient result = queue.CreateConsumer(groupName);
        return Task.FromResult(result);
    }
}