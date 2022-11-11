using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Infrastructure.Data.EntityConfigurations
{
    public class CategoryEntityConfigurate : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
