using Framework.Exceptions.Extensions;
using System.Text;

namespace CleanArchitecture.WebApi.Shared;

public static class StartupExceptionLogger
{
    public static void LogStartupException(Exception ex)
    {
        ex = ex.Demystify();

        try
        {
            var basePath = AppContext.BaseDirectory;
            var logDirectory = Path.Combine(basePath, "logs");

            Directory.CreateDirectory(logDirectory);

            var fileName = $"Fatal_{DateTime.Now:yyyyMMdd_HHmmss_fff}.log";
            var filePath = Path.Combine(logDirectory, fileName);

            var content = new StringBuilder()
                .AppendLine($"Timestamp: {DateTime.Now:O}")
                .AppendLine($"Machine: {Environment.MachineName}")
                .AppendLine($"Process: {Environment.ProcessId}")
                .AppendLine("---- Exception ----")
                .AppendLine(ex.ToString())
                .ToString();

            File.WriteAllText(filePath, content);
        }
        catch
        {
            // DO NOT throw from logger — avoid masking original failure
        }
    }
}
