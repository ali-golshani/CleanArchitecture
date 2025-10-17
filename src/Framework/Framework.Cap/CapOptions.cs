namespace Framework.Cap;

public class CapOptions
{
    public static readonly CapOptions Default = new();

    public int FailedRetryCount { get; set; } = 5;
    public int FailedRetryInterval { get; set; } = 5 * 60;
    public int ConsumerThreadCount { get; set; } = 1;
}
