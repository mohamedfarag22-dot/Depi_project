using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            
            builder.HasOne(o => o.User)
                   .WithMany()
                   .HasForeignKey(o => o.UserId)
                   .IsRequired();


            
            builder.Property(o => o.CreatedDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(o => o.AvailableFrom)
                    .IsRequired();

            builder.Property(o => o.AvailableTo)
                   .IsRequired();

            builder.Property(o => o.Address)
                .HasColumnType("VARCHAR")
                .HasMaxLength(1000).
                IsRequired();


            
            builder.ToTable("Orders");
        }
    }
}
