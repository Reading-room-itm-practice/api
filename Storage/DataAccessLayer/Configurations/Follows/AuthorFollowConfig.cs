using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models.Follows;

namespace Storage.DataAccessLayer.Configurations.Follows
{
    public class AuthorFollowConfig : IEntityTypeConfiguration<AuthorFollow>
    {
        public void Configure(EntityTypeBuilder<AuthorFollow> builder)
        {
            builder.HasOne(c => c.Author)
                .WithMany(f => f.Followers)
                .HasForeignKey(f => f.AuthorId)
                .IsRequired(false);

            builder.HasOne(c => c.Creator)
                .WithMany(f => f.FollowingsAuthors)
                .HasForeignKey(f => f.CreatorId);
        }
    }
}
