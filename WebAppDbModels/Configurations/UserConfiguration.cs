using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebAppDbModels.Models;

namespace WebAppDbModels.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(75);
            builder.Property(u => u.HashPassword)
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}
