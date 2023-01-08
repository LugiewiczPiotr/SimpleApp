using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleApp.Core.Enums;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Infrastructure.Data.EntityConfigurations
{
    public class OrderEntityConfigurate : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
               .Property(x => x.Status)
               .HasConversion(new EnumToStringConverter<OrderStatus>());

            builder.Property(p => p.RowVersion)
               .IsRowVersion();
        }
    }
}
