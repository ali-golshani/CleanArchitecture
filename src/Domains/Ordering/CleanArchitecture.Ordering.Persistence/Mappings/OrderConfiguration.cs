using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Ordering.Persistence;

internal class OrderConfiguration : IEntityTypeConfiguration<Domain.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Order> builder)
    {
        builder.ToTable(Settings.TableNames.Order, Settings.SchemaNames.Order);

        builder.HasKey(x => x.OrderId);

        builder.Property(x => x.Quantity);
        builder.Property(x => x.Price).HasColumnType(Settings.ColumnTypes.Decimal);
        builder.Property(x => x.TrackingCode).HasMaxLength(Settings.StringLengths.TrackingCode);

        builder.ComplexProperty(x => x.Commodity, builder =>
        {
            builder.Property(y => y.CommodityId);
            builder.Property(y => y.CommodityName).HasMaxLength(Settings.StringLengths.CommodityName);
        });
    }
}
