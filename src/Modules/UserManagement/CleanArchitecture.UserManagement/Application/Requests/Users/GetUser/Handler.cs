using CleanArchitecture.UserManagement.Application.Models;
using CleanArchitecture.UserManagement.Persistence;
using Framework.Mediator;
using Framework.Results;
using Framework.Mediator.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.GetUser;

internal sealed class Handler(UserManagementDbContext db) : IRequestHandler<Request, User?>
{
    private readonly UserManagementDbContext db = db;

    public async Task<Result<User?>> Handle(RequestContext<Request> context)
    {
        var request = context.Request;
        var cancellationToken = context.CancellationToken;
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
