using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Infrastructure.Persistence.Configurations;

public sealed class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Subtotal).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.Tax).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.Total).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.PaymentStatus).IsRequired();
        builder.HasOne(x => x.Order).WithOne(x => x.Bill)
            .HasForeignKey<Bill>(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => x.OrderId).IsUnique();
    }
}
