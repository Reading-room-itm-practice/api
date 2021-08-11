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

            var toReadBooksIdList = _context.ReadStatuses.Where(rs => rs.IsWantRead && rs.CreatorId == user.Id).ToList() ;
            var favouriteBooksIdList = _context.ReadStatuses.Where(rs => rs.IsFavorite && rs.CreatorId == user.Id).ToList();
            var areReadBooksIdList = _context.ReadStatuses.Where(rs => rs.IsRead && rs.CreatorId == user.Id).ToList();

            var favouriteBooks = _context.Books.AsEnumerable().Where(b => favouriteBooksIdList.Any(idlist => idlist.BookId == b.Id));
            var toReadBooks = _context.Books.AsEnumerable().Where(b => toReadBooksIdList.Any(idlist => idlist.BookId == b.Id));
            var readBooks = _context.Books.AsEnumerable().Where(b => areReadBooksIdList.Any(idlist => idlist.BookId == b.Id));

            UserProfile profile = BaseProfile(user, toReadBooks, readBooks);

            return user.Id == currentUserId 
                ? CreateMyProfile(profile, favouriteBooks)
                : CreateForeignProfile(profile, user, isFriend, currentUserId, favouriteBooks);
        }

        public UserProfile BaseProfile(User user, IEnumerable<Book> toReadBooks, IEnumerable<Book> readingBooks)
        {
            UserProfile profile = new() { };
            profile.User = user;
            profile.FollowersCount = user.Followers != null ? user.Followers.Count : 0;
            profile.FollowingsCount =
                (user.FollowedAuthors != null ? user.FollowedUsers.Count : 0) + (user.FollowedAuthors != null ? user.FollowedAuthors.Count : 0);

            profile.ToReadBooks = toReadBooks.Any() ? toReadBooks.ToList() : null;
            profile.ReadBooks = readingBooks.Any() ? readingBooks.ToList() : null;
            //profile.Photo = NOT IMPLEMENTED

            return profile;
        }

        public ForeignUserProfile CreateForeignProfile(UserProfile profile, User user, bool isFriend, Guid currentUserId, IEnumerable<Book> favouriteBooks)
        {
            ForeignUserProfile foreignProfile = new ForeignUserProfile(profile);

            foreignProfile.IsFriend = isFriend;
            foreignProfile.IsFollowing = user.Followers != null && user.Followers.Where(f => f.FollowingId == currentUserId).Any() ? true : false;
            foreignProfile.FavouriteBooks = (isFriend && favouriteBooks.Any()) ? favouriteBooks.ToList() : null;

            return foreignProfile;
        }

        public UserProfile CreateMyProfile(UserProfile profile, IEnumerable<Book> favouriteBooks)
        {
            profile.FavouriteBooks = favouriteBooks.Any() ? favouriteBooks.ToList() : null;

            return profile;
        }
    }
}
