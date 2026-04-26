namespace CleanArchitecture.UserManagement.Domain.Repositories;

internal interface IUserRepository
{
    Task<User?> Find(Guid id);
    Task<User?> Find(string username);
    Task<bool> DoesUsernameExist(string username);
    Task<User?> FindByPhoneNumber(string phoneNumber);
    Task<string?> FindUsername(Guid id);
    void Add(User user);
    void Add(UserClaim userClaim);
    void Remove(UserClaim userClaim);
    Task<IReadOnlyCollection<UserClaim>> GetUserClaims(Guid userId);
    Task<IReadOnlyCollection<UserClaim>> GetUserClaims(Guid userId, string claimType);
}