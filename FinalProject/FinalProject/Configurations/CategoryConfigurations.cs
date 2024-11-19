using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd(); 

            
            builder.Property(c => c.Name)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(200)
                   .IsRequired();

            
            builder.Property(c => c.Description)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(1000)
                   .IsRequired(false);

            
            builder.ToTable("Categories");
        }
    }
}
