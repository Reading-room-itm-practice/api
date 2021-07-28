using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(d => d.Description)
                .HasMaxLength(int.MaxValue)
                .IsRequired();
            
            builder.Property(d => d.RelaseDate)
                .HasColumnType("date");

            builder.HasOne(a => a.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(f => f.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.Creator)
              .WithMany(r => r.Books)
              .HasForeignKey(f => f.CreatorId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.Category)
              .WithMany(r => r.Books)
              .HasForeignKey(f => f.CategoryId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.Property(a => a.Approved)
                .HasDefaultValue(false);
        }
    }
}
