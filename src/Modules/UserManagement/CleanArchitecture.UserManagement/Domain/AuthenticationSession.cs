namespace CleanArchitecture.UserManagement.Domain;

internal sealed class AuthenticationSession
{
    private AuthenticationSession() { }

    public AuthenticationSession(
        LoginMethod loginMethod,
        Guid userId,
        string refreshTokenHash,
        DateTime refreshTokenExpirationTime)
    {
        Id = Framework.Utilities.SequentialGuid.NewGuid();
        LoginTime = Shared.SystemClock.Now;
        RowVersion = [];
        LogoutTime = null;

        LoginMethod = loginMethod;
        UserId = userId;
        RefreshTokenHash = refreshTokenHash;
        RefreshTokenExpirationTime = refreshTokenExpirationTime;
    }

    public Guid Id { get; }
    public Guid UserId { get; }

    public DateTime LoginTime { get; }
    public DateTime? LogoutTime { get; private set; }

    public LoginMethod LoginMethod { get; }

    public string RefreshTokenHash { get; private set; }
    public DateTime RefreshTokenExpirationTime { get; private set; }

    public byte[] RowVersion { get; }

    public bool IsLoggedOut => LogoutTime != null;

    public void Logout()
    {
        LogoutTime = DateTime.Now;
    }

    public void UpdateRefreshToken(string refreshTokenHash, DateTime refreshTokenExpirationTime)
    {
        RefreshTokenHash = refreshTokenHash;
        RefreshTokenExpirationTime = refreshTokenExpirationTime;
    }
}

