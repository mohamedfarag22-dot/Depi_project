using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Configurations
{
    public class FeedbackConfigurations : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            
            builder.Property(f => f.Content)
                   .HasColumnType("TEXT") 
                   .IsRequired(); 

            
            builder.HasOne(f => f.User)
                   .WithMany(u => u.Feedbacks) 
                   .HasForeignKey(f => f.UserId)
                   .IsRequired(); 

            builder.HasOne(f => f.Service)
           .WithMany(s => s.Feedbacks)
           .HasForeignKey(f => f.ServiceId)
           .OnDelete(DeleteBehavior.NoAction); 



            
            builder.ToTable("Feedbacks");
        }
    }
}
