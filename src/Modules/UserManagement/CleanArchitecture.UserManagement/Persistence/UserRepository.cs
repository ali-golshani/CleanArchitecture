using CleanArchitecture.UserManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.UserManagement.Persistence;

internal sealed class UserRepository(UserManagementDbContext db) : IUserRepository
{
    public async Task<User?> Find(Guid id)
    {
        return await db.Set<User>().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> Find(string username)
    {
        return await db.Set<User>().SingleOrDefaultAsync(x => x.Username == username);
    }

    public async Task<string?> FindUsername(Guid id)
    {
        return await
            db.Set<User>()
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => x.Username)
            .FirstOrDefaultAsync();
    }

    public void Add(User user)
    {
        db.Set<User>().Add(user);
    }

    public void Add(UserClaim userClaim)
    {
        db.Set<UserClaim>().Add(userClaim);
    }

    public void Remove(UserClaim userClaim)
    {
        db.Set<UserClaim>().Remove(userClaim);
    }

    public async Task<IReadOnlyCollection<UserClaim>> UserClaims(Guid userId)
    {
        return await db.Set<UserClaim>().Where(x => x.UserId == userId).ToListAsync();
    }
}