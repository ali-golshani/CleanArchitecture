using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.UserManagement.Persistence.Repositories;

internal sealed class SessionRepository(UserManagementDbContext db) : ISessionRepository
{
    public async Task<AuthenticationSession?> Find(Guid id)
    {
        return await db.Set<AuthenticationSession>().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AuthenticationSession?> FindByRefreshToken(string refreshTokenHash)
    {
        return await db.Set<AuthenticationSession>().SingleOrDefaultAsync(x => x.RefreshTokenHash == refreshTokenHash);
    }

    public void Add(AuthenticationSession session)
    {
        db.Set<AuthenticationSession>().Add(session);
    }
}
