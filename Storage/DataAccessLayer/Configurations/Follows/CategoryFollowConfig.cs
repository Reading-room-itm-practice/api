﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Models.Follows;

namespace Storage.DataAccessLayer.Configurations.Follows
{
    public class CategoryFollowConfig : IEntityTypeConfiguration<CategoryFollow>
    {
        public void Configure(EntityTypeBuilder<CategoryFollow> builder)
        {
            builder.HasOne(c => c.Category)
                .WithMany(f => f.Followers)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Creator)
                .WithMany(f => f.FollwingsCategories)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
