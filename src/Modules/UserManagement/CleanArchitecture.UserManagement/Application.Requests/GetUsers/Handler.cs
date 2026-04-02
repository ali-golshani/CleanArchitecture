using CleanArchitecture.UserManagement.Application.Requests.Models;
using CleanArchitecture.UserManagement.Persistence;
using Framework.Mediator;
using Framework.Results;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.UserManagement.Application.Requests.GetUsers;

internal sealed class Handler(UserManagementDbContext db) : IRequestHandler<Request, IReadOnlyCollection<User>>
{
    private readonly UserManagementDbContext db = db;

    public async Task<Result<IReadOnlyCollection<User>>> Handle(Request request, CancellationToken cancellationToken)
    {
        var query = db.QuerySet<Domain.User>();

        if (!string.IsNullOrEmpty(request.Name))
        {
            var name = request.Name.Trim();

            query =
                query.Where(x =>
                    x.Username.Contains(name) ||
                    x.FirstName.Contains(name) ||
                    x.LastName.Contains(name) ||
                    (x.FirstName + " " + x.LastName).Contains(name));

        }

        if (!string.IsNullOrEmpty(request.PhoneNumber))
        {
            var phone = request.PhoneNumber.Trim();

            query = query.Where(x => x.PhoneNumber!.Contains(phone));
        }

        var users = await query.ToListAsync(cancellationToken);
        return users.Select(x => x.Convert([])).ToList();
    }
}