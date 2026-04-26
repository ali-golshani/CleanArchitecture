using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Querying.Persistence;

public class EmptyDbContext(DbContextOptions<EmptyDbContext> options) : DbContext(options);
