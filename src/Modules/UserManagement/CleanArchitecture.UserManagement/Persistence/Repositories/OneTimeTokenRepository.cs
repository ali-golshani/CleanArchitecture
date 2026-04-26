using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.UserManagement.Persistence.Repositories;

internal sealed class OneTimeTokenRepository(UserManagementDbContext db) : IOneTimeTokenRepository
{
    public async Task<OneTimeToken?> Find(Guid id)
    {
        return await db.Set<OneTimeToken>().SingleOrDefaultAsync(x => x.Id == id);
    }

    public void Add(OneTimeToken ott)
    {
        db.Set<OneTimeToken>().Add(ott);
    }
}
