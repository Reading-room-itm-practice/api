using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(c => c.Content)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(u => u.User)
                .WithMany(n => n.Notifications)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
