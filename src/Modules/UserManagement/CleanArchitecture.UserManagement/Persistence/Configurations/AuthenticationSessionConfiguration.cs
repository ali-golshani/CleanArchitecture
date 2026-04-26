using CleanArchitecture.UserManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.UserManagement.Persistence.Configurations;

internal sealed class AuthenticationSessionConfiguration : IEntityTypeConfiguration<AuthenticationSession>
{
    public void Configure(EntityTypeBuilder<AuthenticationSession> builder)
    {
        builder.ToTable("Session", Settings.SchemaNames.UserManagement);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId);
        builder.Property(x => x.LoginMethod);
        builder.Property(x => x.LoginTime);
        builder.Property(x => x.LogoutTime);

        builder.Property(x => x.RowVersion).IsRowVersion().IsConcurrencyToken();

        builder
           .Property(x => x.RefreshTokenHash)
           .HasMaxLength(256)
            .HasColumnName("RefreshToken");

        builder.Property(x => x.RefreshTokenExpirationTime);
    }
}