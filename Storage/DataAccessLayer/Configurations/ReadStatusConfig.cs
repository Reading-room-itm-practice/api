using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class ReadStatusConfig : IEntityTypeConfiguration<ReadStatus>
    {
        public void Configure(EntityTypeBuilder<ReadStatus> builder)
        {
            builder.HasKey(r => new { r.BookId, r.UserId });

            builder.HasOne(u => u.User)
                .WithMany(r => r.ReadStatuses)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.User)
                .WithMany(r => r.ReadStatuses)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
