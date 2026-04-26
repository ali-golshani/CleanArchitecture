using DotNetCore.CAP.Internal;
using DotNetCore.CAP.Messages;
using DotNetCore.CAP.Transport;

namespace Framework.Cap.InMemoryTransport;

internal sealed class InMemoryConsumer(InMemoryConsumerGroup group) : IConsumerClient
{
    private readonly InMemoryConsumerGroup group = group;
    private readonly HashSet<string> subscribedTopics = [];

    public Action<LogMessageEventArgs>? OnLogCallback { get; set; }
    public Func<TransportMessage, object?, Task>? OnMessageCallback { get; set; }

    public BrokerAddress BrokerAddress => new("InMemory", string.Empty);
    public bool IsSubscribed(string topic) => subscribedTopics.Contains(topic);

    public Task SubscribeAsync(IEnumerable<string> topics)
    {
        ArgumentNullException.ThrowIfNull(topics);

        foreach (var topic in topics)
        {
            subscribedTopics.Add(topic);
        }

        return Task.CompletedTask;
    }

    public async Task Consume(TransportMessage message)
    {
        var callBack = OnMessageCallback;

        if (callBack is null)
        {
            return;
        }

        await callBack(message, this);
    }

    public Task ListeningAsync(TimeSpan timeout, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            cancellationToken.WaitHandle.WaitOne(timeout);
        }

        return Task.CompletedTask;
    }

    public Task RejectAsync(object? sender)
    {
        if (sender == this)
        {
            throw new PublisherSentFailedException("InMemory Message Consuming Failed: OnMessageCallback Reject");
        }

        return Task.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        group.Remove(this);
        return ValueTask.CompletedTask;
    }

    public Task CommitAsync(object? sender)
    {
        return Task.CompletedTask;
    }
}