using FinalProject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Configurations
{
    public class ServiceConfigurations : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
           
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            
            builder.Property(s => s.Name)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(255)
                   .IsRequired();

           
            builder.Property(s => s.Description)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(255)
                   .IsRequired(false);

       
            builder.Property(s => s.Price)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(100)
                   .IsRequired();

       
            builder.HasOne(s => s.Category)
                   .WithMany(c => c.Services) 
                   .HasForeignKey(s => s.CategoryId)
                   .IsRequired().OnDelete(DeleteBehavior.Cascade);

         
            builder.HasOne(s => s.ServiceProvider)
                   .WithMany(sp => sp.Services)
                    .HasForeignKey(s => s.ServiceProviderId)
                   .IsRequired();

            
            builder.HasMany(s => s.Orders)
                   .WithOne(o => o.Service)
                   .HasForeignKey(o => o.ServiceId)
                   .OnDelete(DeleteBehavior.NoAction);

           
            builder.HasMany(s => s.Feedbacks)
                   .WithOne(f => f.Service)
                   .HasForeignKey(f => f.ServiceId)
                   .OnDelete(DeleteBehavior.NoAction);

          
            builder.ToTable("Services");
        }
    }
}
