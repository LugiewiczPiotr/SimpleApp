using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleApp.Core.Models.Entity;

namespace SimpleApp.Infrastructure.Data.EntityConfigurations
{
    public class OrderItemEnityConfigurate : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(c => c.ProductId)
                .IsRequired();

            builder.Property(c => c.Quantity).HasColumnType("decimal(8,3)")
                .IsRequired();

            builder.HasKey(c => new { c.OrderId, c.ProductId });
        }
    }
}
