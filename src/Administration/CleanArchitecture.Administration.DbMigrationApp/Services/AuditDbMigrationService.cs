using Framework.Persistence;
using Infrastructure.RequestAudit.Persistence;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class AuditDbMigrationService(IServiceProvider serviceProvider)
    : DbMigrationService<AuditDbContext>(serviceProvider);