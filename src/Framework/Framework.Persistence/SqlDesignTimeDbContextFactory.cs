using Framework.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Framework.Persistence;

public abstract class SqlDesignTimeDbContextFactory<TDbContex> : IDesignTimeDbContextFactory<TDbContex>
    where TDbContex : DbContext
{
    protected const string LocalConnectionString = "Server=.;Database=CleanArchitecture;Integrated Security=True;TrustServerCertificate=True;";

    protected abstract TDbContex CreateDbContext(DbContextOptions<TDbContex> options);

    public TDbContex CreateDbContext(string[] args)
    {
        var optionsBuilder = OptionsBuilder();
        return CreateDbContext(optionsBuilder.Options);
    }

    protected virtual DbContextOptionsBuilder<TDbContex> OptionsBuilder()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TDbContex>();
        optionsBuilder.UseSqlServer(
            LocalConnectionString,
            options =>
            {
                options.ConfigureMigrationsHistoryTable();
            });
        return optionsBuilder;
    }
}
