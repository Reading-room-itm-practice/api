using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class FriendRequestConfig : IEntityTypeConfiguration<FriendRequest>
    {
        public void Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            builder.HasOne(c => c.Creator)
                .WithMany(f => f.SentRequests)
                .HasForeignKey(f => f.CreatorId);

            builder.HasOne(c => c.To)
             .WithMany(f => f.RecivedRequests)
             .HasForeignKey(f => f.ToId);
        }
    }
}
