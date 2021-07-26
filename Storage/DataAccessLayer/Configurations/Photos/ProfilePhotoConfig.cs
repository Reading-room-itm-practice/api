using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Identity;
using Storage.Models.Photos;

namespace Storage.DataAccessLayer.Configurations.Photos
{
    class ProfilePhotoConfig : IEntityTypeConfiguration<ProfilePhoto>
    {
        public void Configure(EntityTypeBuilder<ProfilePhoto> builder)
        {
            builder.HasOne(c => c.User)
                .WithOne(l => l.ProfilePhoto)
                .HasForeignKey<User>(f => f.ProfilePhotoId);
        }
    }
}
