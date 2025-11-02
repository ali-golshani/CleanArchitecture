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

    public void Subscribe(IEnumerable<string> topics)
    {
        ArgumentNullException.ThrowIfNull(topics);

        foreach (var topic in topics)
        {
            subscribedTopics.Add(topic);
        }
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

    public void Listening(TimeSpan timeout, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            cancellationToken.WaitHandle.WaitOne(timeout);
        }
    }

    public void Commit(object? sender) { }

    public void Reject(object? sender)
    {
        if (sender == this)
        {
            throw new PublisherSentFailedException("InMemory Message Consuming Failed: OnMessageCallback Reject");
        }
    }

    public void Dispose()
    {
        group.Remove(this);
    }
}