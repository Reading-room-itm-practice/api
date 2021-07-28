using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models.Photos;

namespace Storage.DataAccessLayer.Configurations.Photos
{
    class BookPhotoConfig : IEntityTypeConfiguration<BookPhoto>
    {
        public void Configure(EntityTypeBuilder<BookPhoto> builder)
        {
            builder.HasOne(c => c.Book)
                 .WithMany(l => l.Photos)
                 .HasForeignKey(f => f.BookId)
                 .IsRequired(false);
        }
    }
}
