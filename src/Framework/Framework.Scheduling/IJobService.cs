namespace Framework.Scheduling;

public interface IJobService
{
    Task Execute(CancellationToken stoppingToken);
}
