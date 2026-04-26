using CleanArchitecture.UserManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.UserManagement.Persistence.Configurations;

internal sealed class OneTimeTokenConfiguration : IEntityTypeConfiguration<OneTimeToken>
{
    public void Configure(EntityTypeBuilder<OneTimeToken> builder)
    {
        builder.ToTable("OneTimeToken", Settings.SchemaNames.UserManagement);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId);
        builder.Property(x => x.RegisterTime);
        builder.Property(x => x.ExpirationTime);
        builder.Property(x => x.IsUsed);
        builder.Property(x => x.Purpose);

        builder.Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();

        builder.Property(x => x.TokenHash).HasMaxLength(256);
    }
}