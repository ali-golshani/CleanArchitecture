using CleanArchitecture.UserManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.UserManagement.Persistence.Configurations;

internal sealed class OneTimePasswordConfiguration : IEntityTypeConfiguration<OneTimePassword>
{
    public void Configure(EntityTypeBuilder<OneTimePassword> builder)
    {
        builder.ToTable("OneTimePassword", Settings.SchemaNames.UserManagement);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId);
        builder.Property(x => x.RegisterTime);
        builder.Property(x => x.ExpirationTime);
        builder.Property(x => x.IsUsed);
        builder.Property(x => x.Purpose);
        builder.Property(x => x.Attempts);

        builder.Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();

        builder.Property(x => x.Code).HasMaxLength(10);
    }
}