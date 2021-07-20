using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models.Likes;

namespace Storage.DataAccessLayer.Configurations.Likes
{
    public class LikeConfig : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasDiscriminator(b => b.LikeableType);
        }
    }
}
