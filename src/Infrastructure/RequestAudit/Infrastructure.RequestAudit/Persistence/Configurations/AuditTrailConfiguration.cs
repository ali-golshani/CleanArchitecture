using Infrastructure.RequestAudit.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.RequestAudit.Persistence.Configurations;

internal class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrail>
{
    public void Configure(EntityTypeBuilder<AuditTrail> builder)
    {
        builder.ToTable(TableNames.RequestAudit, SchemaNames.Audit);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Actor).HasMaxLength(256);
        builder.Property(x => x.Domain).HasMaxLength(128);
        builder.Property(x => x.RequestType).HasMaxLength(256);

        builder.Property(x => x.CorrelationId);
        builder.Property(x => x.Request);
        builder.Property(x => x.RequestTime);
        builder.Property(x => x.ResponseTime);
        builder.Property(x => x.IsSuccess);
        builder.Property(x => x.Response);

        builder.ComplexProperty(x => x.Parameters, cb => cb.Configure());

        builder.Ignore(x => x.ShouldLog);
    }
}
