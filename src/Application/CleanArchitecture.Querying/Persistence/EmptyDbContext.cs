using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Querying.Persistence;

public class EmptyDbContext : DbContext
{
    public EmptyDbContext(DbContextOptions<EmptyDbContext> options)
        : base(options)
    { }
}
