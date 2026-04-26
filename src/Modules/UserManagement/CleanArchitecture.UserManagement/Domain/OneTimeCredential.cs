namespace CleanArchitecture.UserManagement.Domain;

internal abstract class OneTimeCredential
{
    private protected OneTimeCredential() { }

    protected OneTimeCredential(Guid userId, DateTime expirationTime)
    {
        Id = Framework.Utilities.SequentialGuid.NewGuid();
        RegisterTime = Shared.SystemClock.Now;
        IsUsed = false;
        RowVersion = [];

        UserId = userId;
        ExpirationTime = expirationTime;
    }

    public Guid Id { get; }
    public Guid UserId { get; }

    public DateTime RegisterTime { get; }
    public DateTime ExpirationTime { get; }

    public byte[] RowVersion { get; }

    public bool IsUsed { get; private set; }

    public void SetAsUsed()
    {
        IsUsed = true;
    }
}

