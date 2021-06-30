using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.DataAccessLayer.Configurations
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
