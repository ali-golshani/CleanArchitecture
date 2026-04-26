using CleanArchitecture.Ordering.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Ordering.Persistence.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(Settings.TableNames.Order, Settings.SchemaNames.Ordering);

        builder.HasKey(x => x.OrderId);
        builder.Property(x => x.OrderId).ValueGeneratedNever();

        builder.Property(x => x.Quantity);
        builder.Property(x => x.CustomerId);
        builder.Property(x => x.BrokerId);
        builder.Property(x => x.Price).HasColumnType(Settings.ColumnTypes.Decimal);
        builder.Property(x => x.TrackingCode).HasMaxLength(Settings.StringLengths.TrackingCode);

        builder.ComplexProperty(x => x.Commodity, builder =>
        {
            builder
            .Property(y => y.CommodityId)
            .HasColumnName(nameof(Commodity.CommodityId));

            builder
            .Property(y => y.CommodityName)
            .HasColumnName(nameof(Commodity.CommodityName))
            .HasMaxLength(Settings.StringLengths.CommodityName);
        });
    }
}
