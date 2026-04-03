namespace CleanArchitecture.UserManagement.Domain;

internal interface IUserRepository
{
    Task<User?> Find(Guid id);
    Task<User?> Find(string username);
    Task<string?> FindUsername(Guid id);
    void Add(User user);
    void Add(UserClaim userClaim);
    void Remove(UserClaim userClaim);
    Task<IReadOnlyCollection<UserClaim>> UserClaims(Guid userId);
}