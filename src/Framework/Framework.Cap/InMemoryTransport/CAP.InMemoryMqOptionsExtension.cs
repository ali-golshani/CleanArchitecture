using DotNetCore.CAP;
using DotNetCore.CAP.Transport;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Cap.InMemoryTransport;

internal sealed class InMemoryQueueOptionsExtension : ICapOptionsExtension
{
    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton(new CapMessageQueueMakerService("InMemoryQueue"));

        services.AddSingleton<InMemoryQueue>();
        services.AddSingleton<IConsumerClientFactory, InMemoryConsumerClientFactory>();
        services.AddSingleton<ITransport, InMemoryTransport>();
    }
}