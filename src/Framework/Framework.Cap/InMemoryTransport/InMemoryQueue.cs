using DotNetCore.CAP.Messages;
using System.Collections.Immutable;

namespace Framework.Cap.InMemoryTransport;

internal sealed class InMemoryQueue
{
    private readonly Lock Lock = new();

    /// <summary>
    /// GroupName => ConsumerGroup
    /// </summary>
    private readonly Dictionary<string, InMemoryConsumerGroup> consumerGroupsDictionary = [];
    private ImmutableList<InMemoryConsumerGroup> consumerGroups = [];

    public InMemoryConsumer CreateConsumer(string groupName)
    {
        lock (Lock)
        {
            if (!consumerGroupsDictionary.TryGetValue(groupName, out var consumerGroup))
            {
                consumerGroup = new InMemoryConsumerGroup(groupName);
                consumerGroupsDictionary.Add(groupName, consumerGroup);
                consumerGroups = consumerGroups.Add(consumerGroup);
            }

            return consumerGroup.CreateConsumer();
        }
    }

    public async Task Send(TransportMessage message)
    {
        var topic = message.GetName();
        foreach (var consumerGroup in consumerGroups)
        {
            var messageCopy = MessageUtility.Copy(message, consumerGroup.GroupName);
            await consumerGroup.Consume(messageCopy, topic);
        }
    }
}
