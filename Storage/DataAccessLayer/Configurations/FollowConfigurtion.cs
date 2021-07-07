using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage.Models;

namespace Storage.DataAccessLayer.Configurations
{
    public class FollowConfigurtion : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasKey(f => new { f.FollowableId, f.FollowableType });
        }
    }
}
