﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApp.Infrastructure.Data.EntityConfigurations
{
    public class ProductEnityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Price)
                .HasColumnType("decimal(9, 2)");

                
            
        }
    }
}