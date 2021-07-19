using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(n => n.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(b => b.Biography)
                .HasMaxLength(int.MaxValue)
                .IsRequired();
        }
    }
}
