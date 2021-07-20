using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models.Follows;

namespace Storage.DataAccessLayer.Configurations.Follows
{
    public class FollowConfig : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasDiscriminator(b => b.FollowableType);
        }
    }
}
