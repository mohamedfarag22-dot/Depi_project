using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Configurations
{
    public class ChatConfigurations : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            
            builder.Property(c => c.Message)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(1000)  
                   .IsRequired();

            
            builder.HasOne(c => c.Sender)
                   .WithMany(u => u.SentMessages)
                   .HasForeignKey(c => c.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Receiver)
                   .WithMany(sp => sp.ReceivedMessages)
                   .HasForeignKey(c => c.ReceiverId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.ToTable("Chats");
        }
    }
}
