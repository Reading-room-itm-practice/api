using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations.Photos
{
    public class PhotoConfig : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.Property(c => c.Path)
                   .IsRequired();

            builder.HasOne(c => c.Creator)
                   .WithMany(l => l.Photos)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
