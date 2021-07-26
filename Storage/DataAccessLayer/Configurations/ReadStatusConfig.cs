using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class ReadStatusConfig : IEntityTypeConfiguration<ReadStatus>
    {
        public void Configure(EntityTypeBuilder<ReadStatus> builder)
        {
            builder.HasOne(u => u.Creator)
                .WithMany(r => r.ReadStatuses)
                .HasForeignKey(f => f.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Book)
              .WithMany(r => r.ReadStatuses);
        }
    }
}
