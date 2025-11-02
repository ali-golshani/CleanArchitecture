using DotNetCore.CAP.Messages;

namespace Framework.Cap.InMemoryTransport;

internal sealed class InMemoryConsumerGroup(string groupName)
{
    private readonly Lock Lock = new();
    private readonly List<InMemoryConsumer> consumers = [];

    public string GroupName { get; } = groupName;

    public InMemoryConsumer CreateConsumer()
    {
        var consumer = new InMemoryConsumer(this);
        Add(consumer);
        return consumer;
    }

    public async Task Consume(TransportMessage message, string topic)
    {
        var consumer = FindSubscribedConsumer(topic);

        if (consumer is null)
        {
            return;
        }

        await consumer.Consume(message);
    }

    private InMemoryConsumer? FindSubscribedConsumer(string topic)
    {
        lock (Lock)
        {
            return consumers.Find(x => x.IsSubscribed(topic));
        }
    }

    public void Remove(InMemoryConsumer consumer)
    {
        lock (Lock)
        {
            consumers.Remove(consumer);
        }
    }

    private void Add(InMemoryConsumer consumer)
    {
        lock (Lock)
        {
            consumers.Add(consumer);
        }
    }
}
