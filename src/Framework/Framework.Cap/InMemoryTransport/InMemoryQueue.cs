using DotNetCore.CAP.Messages;
using System.Collections.Immutable;

namespace Framework.Cap.InMemoryTransport;

internal sealed class InMemoryQueue
{
    private readonly Lock Lock = new();

    /// <summary>
    /// GroupName => ConsumerGroup
    /// </summary>
    private readonly Dictionary<string, InMemoryConsumerGroup> consumerGroups = [];
    private ImmutableList<InMemoryConsumerGroup> consumerGroupsList = [];

    public InMemoryConsumer CreateConsumer(string groupName)
    {
        lock (Lock)
        {
            if (!consumerGroups.TryGetValue(groupName, out var consumerGroup))
            {
                consumerGroup = new InMemoryConsumerGroup(groupName);
                consumerGroups.Add(groupName, consumerGroup);
                consumerGroupsList = consumerGroupsList.Add(consumerGroup);
            }

            return consumerGroup.CreateConsumer();
        }
    }

    public async Task Send(TransportMessage message)
    {
        var topic = message.GetName();
        foreach (var consumerGroup in consumerGroupsList)
        {
            var messageCopy = Copy(message, consumerGroup.GroupName);
            await consumerGroup.Consume(messageCopy, topic);
        }
    }

    private static TransportMessage Copy(TransportMessage message, string groupName)
    {
        return new TransportMessage
        (
            message.Headers.ToDictionary(o => o.Key, o => o.Value),
            message.Body
        )
        {
            Headers =
            {
                [Headers.Group] = groupName,
            }
        };
    }
}
