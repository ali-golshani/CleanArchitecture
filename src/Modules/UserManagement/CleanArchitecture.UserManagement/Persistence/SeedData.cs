using CleanArchitecture.UserManagement.Domain;

namespace CleanArchitecture.UserManagement.Persistence;

public sealed class SeedData(UserManagementDbContext db)
{
    private readonly UserManagementDbContext db = db;

    public async Task SeedAdminUser()
    {
        var username = "admin";
        var password = "@admin";

        var hashedPassword = Utilities.PasswordHasher.Hash(username, password);

        var user = new User
        (
            username: username,
            firstName: "Ali",
            lastName: "Golshani",
            phoneNumber: "1234567890",
            passwordHash: hashedPassword
        );

        var claim = new UserClaim
        {
            UserId = user.Id,
            ClaimType = UserClaimTypes.Role,
            ClaimValue = "developer"
        };

        db.Set<User>().Add(user);
        db.Set<UserClaim>().Add(claim);

        await db.SaveChangesAsync();
    }
}
