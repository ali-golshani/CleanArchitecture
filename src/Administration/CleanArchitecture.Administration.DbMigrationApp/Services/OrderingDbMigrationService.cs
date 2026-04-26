using CleanArchitecture.Ordering.Persistence;
using Framework.Persistence;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class OrderingDbMigrationService(IServiceProvider serviceProvider)
    : DbMigrationService<OrderingDbContext>(serviceProvider);
