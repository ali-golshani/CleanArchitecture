namespace CleanArchitecture.UserManagement.Domain;

internal sealed class OneTimeToken : OneTimeCredential
{
    private OneTimeToken() { }

    public OneTimeToken(OneTimeTokenPurpose purpose, Guid userId, string tokenHash, DateTime expirationTime)
        : base(userId, expirationTime)
    {
        Purpose = purpose;
        TokenHash = tokenHash;
    }

    public string TokenHash { get; }
    public OneTimeTokenPurpose Purpose { get; }
}

