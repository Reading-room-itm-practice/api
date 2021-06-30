using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DataAccessLayer.Configurations
{
    public class ReadStatusConfiguration : IEntityTypeConfiguration<ReadStatus>
    {
        public void Configure(EntityTypeBuilder<ReadStatus> builder)
        {
            builder.HasKey(r => new { r.BookId, r.UserId });
        }
    }
}
