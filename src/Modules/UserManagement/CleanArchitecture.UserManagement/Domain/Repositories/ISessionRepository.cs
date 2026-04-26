namespace CleanArchitecture.UserManagement.Domain.Repositories;

internal interface ISessionRepository
{
    Task<AuthenticationSession?> Find(Guid id);
    Task<AuthenticationSession?> FindByRefreshToken(string refreshTokenHash);
    void Add(AuthenticationSession session);
}
