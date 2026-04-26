using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Framework.MassTransit;

public class BusHostedService(IBusControl bus) : IHostedService
{
    readonly IBusControl bus = bus;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return bus.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return bus.StopAsync(cancellationToken);
    }
}