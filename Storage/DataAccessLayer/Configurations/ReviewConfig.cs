using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(c => c.Content)
                .HasMaxLength(int.MaxValue)
                .IsRequired();

            builder.HasOne(c => c.Creator)
                   .WithMany(f => f.Reviews)
                   .HasForeignKey(f => f.CreatorId);

            builder.HasOne(c => c.Book)
               .WithMany(f => f.Reviews)
               .HasForeignKey(f => f.BookId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
