using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models.Likes;

namespace Storage.DataAccessLayer.Configurations.Likes
{
    public class ReviewLikeConfig : IEntityTypeConfiguration<ReviewLike>
    {
        public void Configure(EntityTypeBuilder<ReviewLike> builder)
        {
            builder.HasOne(r => r.Review)
                .WithMany(l => l.Likes)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Creator)
                .WithMany(l => l.ReviewLikes)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Review)
               .WithMany(f => f.Likes)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
