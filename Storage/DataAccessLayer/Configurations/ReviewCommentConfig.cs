using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class ReviewCommentConfig : IEntityTypeConfiguration<ReviewComment>
    {
        public void Configure(EntityTypeBuilder<ReviewComment> builder)
        {
            builder.Property(c => c.Content)
                .HasMaxLength(2000)
                .IsRequired();

            builder.HasOne(c => c.Creator)
                .WithMany(f => f.ReviewComments)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Review)
               .WithMany(f => f.Comments)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
