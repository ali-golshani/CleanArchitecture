using DotNetCore.CAP.Messages;

namespace Framework.Cap.InMemoryTransport;

internal static class MessageUtility
{
    public static TransportMessage Copy(TransportMessage message, string groupName)
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