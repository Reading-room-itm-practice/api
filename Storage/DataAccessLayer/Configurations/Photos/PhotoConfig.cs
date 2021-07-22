using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models.Photos;

namespace Storage.DataAccessLayer.Configurations.Photos
{
    public class PhotoConfig : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasDiscriminator(t => t.PhotoType);

            builder.Property(c => c.Path)
                   .IsRequired();

            builder.HasOne(c => c.Creator)
                   .WithMany(l => l.AddedPhotos);

            builder.HasDiscriminator(b => b.PhotoType)
                .HasValue<AuthorPhoto>(PhotoTypes.AuthorPhoto)
                .HasValue<BookPhoto>(PhotoTypes.BookPhoto)
                .HasValue<ProfilePhoto>(PhotoTypes.ProfilePhoto);
        }
    }
}
