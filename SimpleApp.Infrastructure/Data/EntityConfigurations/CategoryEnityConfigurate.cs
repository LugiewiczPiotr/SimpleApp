using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApp.Infrastructure.Data.EntityConfigurations
{
    class CategoryEnityConfigurate : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
