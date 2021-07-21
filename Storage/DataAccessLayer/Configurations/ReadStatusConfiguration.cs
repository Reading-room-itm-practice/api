using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class ReadStatusConfiguration : IEntityTypeConfiguration<ReadStatus>
    {
        public void Configure(EntityTypeBuilder<ReadStatus> builder)
        {
            builder.HasKey(r => new { r.BookId, r.UserId });
        }
    }
}
