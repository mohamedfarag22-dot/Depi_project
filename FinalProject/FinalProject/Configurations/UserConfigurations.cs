
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            

            
            builder.Property(u => u.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(500).IsRequired();

            builder.Property(u => u.Phone)
                .HasColumnType("VARCHAR")
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(u => u.Address)
                .HasColumnType("VARCHAR")
                .HasMaxLength(1000).
                IsRequired(false);

            

           
            

        }
    }
}
