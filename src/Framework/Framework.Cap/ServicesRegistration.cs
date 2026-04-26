using Framework.Cap.InMemoryTransport;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Cap;

public static class ServicesRegistration
{
    public static void AddCapMessaging(this IServiceCollection services, CapOptions options, string dbConnectionString)
    {
        services.AddCap(
            x =>
            {
                x.UseSqlServer(dbConnectionString);
                x.UseInMemoryMessageQueue();
                x.UseDashboard();

                x.FailedRetryCount = options.FailedRetryCount ?? x.FailedRetryCount;
                x.FailedRetryInterval = options.FailedRetryInterval ?? x.FailedRetryInterval;
                x.ConsumerThreadCount = options.ConsumerThreadCount ?? x.ConsumerThreadCount;
                x.SucceedMessageExpiredAfter = options.SucceedMessageExpiredAfter ?? x.SucceedMessageExpiredAfter;
                x.FailedMessageExpiredAfter = options.FailedMessageExpiredAfter ?? x.FailedMessageExpiredAfter;
                x.EnablePublishParallelSend = options.EnablePublishParallelSend ?? x.EnablePublishParallelSend;
                x.FallbackWindowLookbackSeconds = options.FallbackWindowLookbackSeconds ?? x.FallbackWindowLookbackSeconds;
                x.CollectorCleaningInterval = options.CollectorCleaningInterval ?? x.CollectorCleaningInterval;
                x.SchedulerBatchSize = options.SchedulerBatchSize ?? x.SchedulerBatchSize;
                x.UseStorageLock = options.UseStorageLock ?? x.UseStorageLock;
            });

        ServicesConfiguration.RegisterCapEventOutbox(services);
    }
}
