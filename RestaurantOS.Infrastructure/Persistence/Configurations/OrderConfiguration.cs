using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Infrastructure.Persistence.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAtUtc).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.HasOne(x => x.RestaurantTable).WithMany(x => x.Orders)
            .HasForeignKey(x => x.RestaurantTableId).OnDelete(DeleteBehavior.Restrict);
    }
}
