using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models.Likes;

namespace Storage.DataAccessLayer.Configurations.Likes
{
    public class ReviewCommentLikeConfig : IEntityTypeConfiguration<ReviewCommentLike>
    {
        public void Configure(EntityTypeBuilder<ReviewCommentLike> builder)
        {
            builder.HasOne(r => r.ReviewComment)
                .WithMany(l => l.Likes)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Liker)
                .WithMany(l => l.CommentLikes)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
