using CleanArchitecture.UserManagement.Application.Requests.Models;
using CleanArchitecture.UserManagement.Persistence;
using Framework.Mediator;
using Framework.Results;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.UserManagement.Application.Requests.GetUser;

internal sealed class Handler(UserManagementDbContext db) : IRequestHandler<Request, User?>
{
    private readonly UserManagementDbContext db = db;

    public async Task<Result<User?>> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = await
            db.QuerySet<Domain.User>()
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return Result<User?>.Success(null);
        }

        var claims = await
            db.QuerySet<Domain.UserClaim>()
            .Where(x => x.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        return user.Convert(claims);
    }
}