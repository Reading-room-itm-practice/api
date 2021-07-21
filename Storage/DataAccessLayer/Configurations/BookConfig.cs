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
                .HasForeignKey(f => f.AuthorId);

            builder.HasOne(u => u.Creator)
              .WithMany(r => r.Books)
              .HasForeignKey(f => f.CreatorId);

            builder.HasOne(u => u.Category)
              .WithMany(r => r.Books)
              .HasForeignKey(f => f.CategoryId);
        }
    }
}
