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
        }
    }
}
