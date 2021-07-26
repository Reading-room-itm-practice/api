using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models.Likes;

namespace Storage.DataAccessLayer.Configurations.Likes
{
    public class ReviewCommentLikeConfig : IEntityTypeConfiguration<ReviewCommentLike>
    {
        public void Configure(EntityTypeBuilder<ReviewCommentLike> builder)
        {
            builder.HasOne(c => c.Creator)
                .WithMany(l => l.CommentLikes)
                .HasForeignKey(f => f.CreatorId);

            builder.HasOne(c => c.ReviewComment)
               .WithMany(f => f.Likes)
               .HasForeignKey(f => f.ReviewCommentId)
               .IsRequired(false);
        }
    }
}
