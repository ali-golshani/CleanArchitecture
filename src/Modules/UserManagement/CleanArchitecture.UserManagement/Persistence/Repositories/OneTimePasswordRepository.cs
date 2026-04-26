using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.UserManagement.Persistence.Repositories;

internal sealed class OneTimePasswordRepository(UserManagementDbContext db) : IOneTimePasswordRepository
{
    public async Task<OneTimePassword?> Find(Guid id)
    {
        return await db.Set<OneTimePassword>().SingleOrDefaultAsync(x => x.Id == id);
    }

    public void Add(OneTimePassword otp)
    {
        db.Set<OneTimePassword>().Add(otp);
    }
}
