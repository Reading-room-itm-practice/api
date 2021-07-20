using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(d => d.Description)
                .HasMaxLength(int.MaxValue)
                .IsRequired();

            builder.Property(r => r.ReleaseYear)
                .HasPrecision(4, 0);

            builder.HasOne(a => a.Author)
                .WithMany(b => b.Books)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
