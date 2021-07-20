using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class FriendRequestConfig : IEntityTypeConfiguration<FriendRequest>
    {
        public void Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            
            builder.HasOne(c => c.From)
                .WithMany(f => f.SentRequests)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.To)
             .WithMany(f => f.RecivedRequests)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
