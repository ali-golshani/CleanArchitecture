using DurableTask.SqlServer;

namespace Framework.DurableTask;

internal static class OrchestrationServiceFactory
{
    public static SqlOrchestrationService GetSqlOrchestrationService(SqlOptions sqlOptions, DurableTaskOptions options)
    {
        var storageSettings = new SqlOrchestrationServiceSettings
        (
            connectionString: sqlOptions.ConnectionString,
            taskHubName: Settings.TaskHubName,
            schemaName: Settings.Persistence.SchemaName
        )
        {
            CreateDatabaseIfNotExists = false,
        };

        storageSettings.WorkItemBatchSize = options.WorkItemBatchSize ?? storageSettings.WorkItemBatchSize;
        storageSettings.WorkItemLockTimeout = options.WorkItemLockTimeout ?? storageSettings.WorkItemLockTimeout;
        storageSettings.AppName = options.AppName ?? storageSettings.AppName;
        storageSettings.MaxConcurrentActivities = options.MaxConcurrentActivities ?? storageSettings.MaxConcurrentActivities;
        storageSettings.MaxActiveOrchestrations = options.MaxActiveOrchestrations ?? storageSettings.MaxActiveOrchestrations;
        storageSettings.MinOrchestrationPollingInterval = options.MinOrchestrationPollingInterval ?? storageSettings.MinOrchestrationPollingInterval;
        storageSettings.MaxOrchestrationPollingInterval = options.MaxOrchestrationPollingInterval ?? storageSettings.MaxOrchestrationPollingInterval;
        storageSettings.DeltaBackoffOrchestrationPollingInterval = options.DeltaBackoffOrchestrationPollingInterval ?? storageSettings.DeltaBackoffOrchestrationPollingInterval;
        storageSettings.MinActivityPollingInterval = options.MinActivityPollingInterval ?? storageSettings.MinActivityPollingInterval;
        storageSettings.MaxActivityPollingInterval = options.MaxActivityPollingInterval ?? storageSettings.MaxActivityPollingInterval;
        storageSettings.DeltaBackoffActivityPollingInterval = options.DeltaBackoffActivityPollingInterval ?? storageSettings.DeltaBackoffActivityPollingInterval;

        return new SqlOrchestrationService(storageSettings);
    }
}