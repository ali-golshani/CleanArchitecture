using CleanArchitecture.UserManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.UserManagement.Persistence.Configurations;

internal sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable("UserClaim", "UserManagement");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId);

        builder
            .Property(x => x.ClaimType)
            .HasMaxLength(256)
            .IsRequired(false);

        builder
            .Property(x => x.ClaimValue)
            .HasMaxLength(256)
            .IsRequired(false);
    }
}