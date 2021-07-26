using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models.Photos;

namespace Storage.DataAccessLayer.Configurations.Photos
{
    public class AuthorPhotoConfig : IEntityTypeConfiguration<AuthorPhoto>
    {
        public void Configure(EntityTypeBuilder<AuthorPhoto> builder)
        {
            builder.HasOne(c => c.Author)
                .WithMany(l => l.Photos)
                .HasForeignKey(f => f.AuthorId)
                .IsRequired(false); ;
        }
    }
}
