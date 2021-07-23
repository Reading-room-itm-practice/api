using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Storage.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Storage.Models;
using Storage.Interfaces;
using Storage.Models.Follows;
using Storage.Models.Likes;
using Microsoft.AspNetCore.Identity;
using Storage.Models.Photos;

namespace Storage.DataAccessLayer
{
    public class ApiDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
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
        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<CategoryFollow> CategoryFollows { get; set; }
        public DbSet<AuthorFollow> AuthorFollows { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<ReviewCommentLike> CommentLikes { get; set; }
        public DbSet<ReviewLike> ReviewLikes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }
        public DbSet<BookPhoto> BookPhotos { get; set; }
        public DbSet<AuthorPhoto> AuthorPhotos { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ReadStatus> ReadStatuses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewComment> ReviewComments { get; set; }

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
                        entry.Entity.CreatorId = _loggedUserProvider.GetUserId();
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdaterId = _loggedUserProvider.GetUserId();
                        entry.Entity.LastModifiedAt = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
