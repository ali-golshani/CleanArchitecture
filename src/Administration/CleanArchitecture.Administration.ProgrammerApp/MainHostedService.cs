using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.Administration.ProgrammerApp;

public class MainHostedService(IServiceProvider serviceProvider) : BasicApp(serviceProvider), IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        await Service<MainApp>().Run();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}