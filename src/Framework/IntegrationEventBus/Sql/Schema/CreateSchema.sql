SET XACT_ABORT ON;
BEGIN TRANSACTION;

DECLARE @lockResult INT;
EXEC @lockResult = sys.sp_getapplock
    @Resource = N'IntegrationEventBus:Schema:cap',
    @LockMode = 'Exclusive',
    @LockOwner = 'Transaction',
    @LockTimeout = 30000;

IF @lockResult < 0
    THROW 50000, 'Could not acquire the IntegrationEventBus schema lock.', 1;

IF SCHEMA_ID(N'cap') IS NULL
    EXEC(N'CREATE SCHEMA [cap]');

IF OBJECT_ID(N'[cap].[Events]', N'U') IS NULL
BEGIN
    CREATE TABLE [cap].[Events]
    (
        [Sequence] BIGINT IDENTITY(1,1) NOT NULL,
        [Id] UNIQUEIDENTIFIER NOT NULL,
        [EventName] NVARCHAR(200) NOT NULL,
        [Topic] NVARCHAR(200) NOT NULL,
        [PayloadJson] NVARCHAR(MAX) NOT NULL,
        [OccurredAtUtc] DATETIMEOFFSET(7) NOT NULL,
        [CreatedAtUtc] DATETIMEOFFSET(7) NOT NULL,
        [CorrelationId] NVARCHAR(200) NULL,
        [CausationId] UNIQUEIDENTIFIER NULL,
        CONSTRAINT [PK_IntegrationEventBus_Events] PRIMARY KEY CLUSTERED ([Sequence]),
        CONSTRAINT [UQ_IntegrationEventBus_Events_Id] UNIQUE ([Id])
    );
END;

IF OBJECT_ID(N'[cap].[Deliveries]', N'U') IS NULL
BEGIN
    CREATE TABLE [cap].[Deliveries]
    (
        [Id] BIGINT IDENTITY(1,1) NOT NULL,
        [EventId] UNIQUEIDENTIFIER NOT NULL,
        [SubscriptionName] NVARCHAR(200) NOT NULL,
        [Status] TINYINT NOT NULL,
        [AttemptCount] INT NOT NULL,
        [BlocksFollowing] BIT NOT NULL,
        [NextAttemptAtUtc] DATETIMEOFFSET(7) NULL,
        [FirstFailedAtUtc] DATETIMEOFFSET(7) NULL,
        [LastAttemptAtUtc] DATETIMEOFFSET(7) NULL,
        [CompletedAtUtc] DATETIMEOFFSET(7) NULL,
        [LastError] NVARCHAR(MAX) NULL,
        [RetryPolicyName] NVARCHAR(200) NOT NULL,
        [RetryPolicyVersion] INT NOT NULL,
        CONSTRAINT [PK_IntegrationEventBus_Deliveries] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_IntegrationEventBus_Deliveries_Events]
            FOREIGN KEY ([EventId]) REFERENCES [cap].[Events]([Id]),
        CONSTRAINT [UQ_IntegrationEventBus_Deliveries_Event_Subscription]
            UNIQUE ([EventId], [SubscriptionName])
    );

    CREATE INDEX [IX_IntegrationEventBus_Deliveries_Ready]
        ON [cap].[Deliveries]
        ([SubscriptionName], [Status], [BlocksFollowing], [NextAttemptAtUtc])
        INCLUDE ([EventId], [AttemptCount]);
END;

COMMIT TRANSACTION;
