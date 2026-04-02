using CleanArchitecture.UserManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.UserManagement.Persistence;

public sealed class SeedData(UserManagementDbContext db)
{
    private readonly UserManagementDbContext db = db;

    public async Task SeedAdmin()
    {
        var userId = Guid.Parse("565A3A39-ED6F-4B00-9999-62F516032F3A");
        var username = "admin";
        var password = "@admin";

        if (await db.Set<User>().AnyAsync(x => x.Id == userId))
        {
            return;
        }

        var hashedPassword = Utilities.PasswordHasher.Hash(username, password);

        var user = new User
        (
            id: userId,
            username: username,
            firstName: "Ali",
            lastName: "Golshani",
            phoneNumber: "1234567890",
            passwordHash: hashedPassword
        );

        var roleClaim = new UserClaim
        {
            UserId = user.Id,
            ClaimType = UserClaimTypes.Role,
            ClaimValue = "developer"
        };

        var permissionClaim = new UserClaim
        {
            UserId = user.Id,
            ClaimType = UserClaimTypes.Permission,
            ClaimValue = "UserManagement"
        };

        db.Set<User>().Add(user);
        db.Set<UserClaim>().Add(roleClaim);
        db.Set<UserClaim>().Add(permissionClaim);

        await db.SaveChangesAsync();
    }
}
