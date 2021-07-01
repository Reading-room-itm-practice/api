﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Common;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebAPI.Interfaces.User;
using WebAPI.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.DataAccessLayer
{
    public class ApiDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private readonly ILoggedUserProvider _loggedUserProvider;
        public ApiDbContext(DbContextOptions<ApiDbContext> options, ILoggedUserProvider loggedUserProvider) : base(options)
        {
            _loggedUserProvider = loggedUserProvider;
        }

        public override DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ReadStatus> ReadStatuses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewComment> ReviewComments { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<AuditableModel> entry in ChangeTracker.Entries<AuditableModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _loggedUserProvider.UserId;
                        entry.Entity.Created = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _loggedUserProvider.UserId;
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
