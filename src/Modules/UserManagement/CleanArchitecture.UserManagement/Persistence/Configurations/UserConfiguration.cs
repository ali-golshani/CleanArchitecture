using CleanArchitecture.UserManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.UserManagement.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", Settings.SchemaNames.UserManagement);

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.FirstName)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(x => x.PhoneNumber)
            .HasMaxLength(16)
            .IsRequired(false);

        builder
            .Property(x => x.Username)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .HasIndex(x => x.Username)
            .IsUnique();

        builder
            .Property(x => x.PasswordHash)
            .HasMaxLength(256)
            .HasColumnName("Passsword")
            .IsRequired(false);

        builder.Property(x => x.IsActive);
    }
}