using Core.DTOs;
using Core.Interfaces;
using Storage.DataAccessLayer;
using Storage.Identity;
using Storage.Interfaces;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApiDbContext _context;
        private readonly ILoggedUserProvider _loggedUserProvider;
        public ProfileRepository(ApiDbContext context, ILoggedUserProvider loggedUserProvider)
        {
            _context = context;
            _loggedUserProvider = loggedUserProvider;
        }

        public UserProfile GetProfile(User user, bool isFriend = false)
        {
            var currentUserId = _loggedUserProvider.GetUserId();

            var wantReadIds = _context.ReadStatuses.Where(rs => rs.IsWantRead && rs.CreatorId == user.Id).Select(rs => rs.BookId).ToList();
            var favouriteIds = _context.ReadStatuses.Where(rs => rs.IsFavorite && rs.CreatorId == user.Id).Select(rs => rs.BookId).ToList();
            var readIds = _context.ReadStatuses.Where(rs => rs.IsRead && rs.CreatorId == user.Id).Select(rs => rs.BookId).ToList();

            var wantReadBooks = _context.Books.Where(b => wantReadIds.Any(id => id == b.Id)).ToList();
            var favouriteBooks = _context.Books.Where(b => favouriteIds.Any(id => id == b.Id)).ToList();
            var readBooks = _context.Books.Where(b => readIds.Any(id => id == b.Id)).ToList();

            UserProfile profile = BaseProfile(user, wantReadBooks, readBooks);

            return user.Id == currentUserId
                ? CreateMyProfile(profile, favouriteBooks)
                : CreateForeignProfile(profile, user, isFriend, currentUserId, favouriteBooks);
        }

        public UserProfile BaseProfile(User user, IEnumerable<Book> toReadBooks, IEnumerable<Book> readingBooks)
        {
            UserProfile profile = new()
            {
                User = user,
                FollowersCount = user.Followers?.Count ?? 0,
                FollowingsCount = (user.FollowedUsers?.Count ?? 0) + (user.FollowedAuthors?.Count ?? 0),
                ToReadBooks = toReadBooks,
                ReadBooks = readingBooks
                //Photo = NOT IMPLEMENTED
            };

            return profile;
        }

        public ForeignUserProfile CreateForeignProfile(UserProfile profile, User user, bool isFriend, Guid currentUserId, List<Book> favouriteBooks)
        {
            ForeignUserProfile foreignProfile = new(profile)
            {
                IsFriend = isFriend,
                IsFollowing = user.Followers?.Any(f => f.FollowingId == currentUserId) ?? false,
                FavouriteBooks = isFriend ? favouriteBooks : new(),
            };

            return foreignProfile;
        }

        public UserProfile CreateMyProfile(UserProfile profile, IEnumerable<Book> favouriteBooks)
        {
            profile.FavouriteBooks = favouriteBooks;

            return profile;
        }
    }
}
