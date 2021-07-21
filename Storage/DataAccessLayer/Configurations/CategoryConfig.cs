using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(n => n.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(c => c.Creator)
                .WithMany(a => a.Categories)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
