namespace CleanArchitecture.UserManagement.Domain;

internal sealed class User
{
    private User() { }

    public User(
        string username,
        string firstName,
        string lastName,
        string phoneNumber,
        string passwordHash)
    {
        Id = Guid.NewGuid();
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;

        IsActive = true;
    }

    public Guid Id { get; }
    public string Username { get; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string PasswordHash { get; private set; }

    public bool IsActive { get; private set; }

    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiry { get; private set; }

    public string Name => $"{FirstName} {LastName}";

    public void Update(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    public void UpdatePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void UpdateRefreshToken(string refreshToken, DateTime refreshTokenExpiry)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiry = refreshTokenExpiry;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Inactivate()
    {
        IsActive = false;
    }
}