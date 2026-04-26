namespace CleanArchitecture.UserManagement.Domain;

internal sealed class OneTimePassword : OneTimeCredential
{
    private OneTimePassword() { }

    public OneTimePassword(OneTimePasswordPurpose purpose, Guid userId, string code, DateTime expirationTime)
        : base(userId, expirationTime)
    {
        Code = code;
        Purpose = purpose;

        Attempts = 0;
    }

    public string Code { get; }
    public OneTimePasswordPurpose Purpose { get; }
    public int Attempts { get; private set; }

    public void AttemptFailed()
    {
        Attempts++;
    }
}

