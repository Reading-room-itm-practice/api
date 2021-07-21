﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models.Follows;

namespace Storage.DataAccessLayer.Configurations.Follows
{
    class UserFollowConfig : IEntityTypeConfiguration<UserFollow>
    {
        public void Configure(EntityTypeBuilder<UserFollow> builder)
        {
            builder.HasOne(c => c.Creator)
                .WithMany(f => f.Followings)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Following)
                .WithMany(f => f.Followers)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
