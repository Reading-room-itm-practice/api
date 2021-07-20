using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class SuggestionConfig : IEntityTypeConfiguration<Suggestion>
    {
        public void Configure(EntityTypeBuilder<Suggestion> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Author)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Comment)
                .HasMaxLength(400);
        }
    }
}
