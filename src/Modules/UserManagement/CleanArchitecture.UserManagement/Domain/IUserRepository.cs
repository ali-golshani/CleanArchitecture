namespace CleanArchitecture.UserManagement.Domain;

internal interface IUserRepository
{
    Task<User?> Find(Guid id);
    Task<User?> Find(string username);
    Task<string?> FindUsername(Guid id);
    Task Add(User user);
    Task Add(UserClaim userClaim);
    Task<IReadOnlyCollection<UserClaim>> UserClaims(Guid userId);
}