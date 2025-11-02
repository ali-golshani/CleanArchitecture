using DotNetCore.CAP;
using DotNetCore.CAP.Internal;
using DotNetCore.CAP.Messages;
using DotNetCore.CAP.Transport;
using Microsoft.Extensions.Logging;

namespace Framework.Cap.InMemoryTransport;

internal sealed class InMemoryTransport(InMemoryQueue queue, ILogger<InMemoryTransport> logger) : ITransport
{
    private readonly ILogger logger = logger;

    public BrokerAddress BrokerAddress => new("InMemory", string.Empty);

    public async Task<OperateResult> SendAsync(TransportMessage message)
    {
        try
        {
            await queue.Send(message);
            logger.LogDebug("Event message [{@Message}] has been published.", message.GetName());
            return OperateResult.Success;
        }
        catch (Exception ex)
        {
            var wrapperEx = new PublisherSentFailedException(ex.Message, ex);
            return OperateResult.Failed(wrapperEx);
        }
    }
}