using CleanArchitecture.Audit.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Audit.Persistence;

internal class CommandAuditTrailConfiguration : IEntityTypeConfiguration<CommandAuditTrail>
{
    public void Configure(EntityTypeBuilder<CommandAuditTrail> builder)
    {
        builder.ToTable(TableNames.CommandAudit, SchemaNames.Audit);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Actor).HasMaxLength(256);
        builder.Property(x => x.Domain).HasMaxLength(128);
        builder.Property(x => x.Command).HasMaxLength(256);

        builder.Property(x => x.CorrelationId);
        builder.Property(x => x.Request);
        builder.Property(x => x.RequestTime);
        builder.Property(x => x.ResponseTime);
        builder.Property(x => x.IsSuccess);
        builder.Property(x => x.Response);

        builder.ComplexProperty(x => x.Parameters, cb => cb.Configure());
    }
}
