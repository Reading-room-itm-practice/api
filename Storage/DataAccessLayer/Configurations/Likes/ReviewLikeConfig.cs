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
                .WithMany(l => l.Likes);

            builder.HasOne(c => c.Creator)
                .WithMany(l => l.ReviewLikes);

            builder.HasOne(c => c.Review)
               .WithMany(f => f.Likes)
               .HasForeignKey(f => f.ReviewId)
               .IsRequired(false);
        }
    }
}
