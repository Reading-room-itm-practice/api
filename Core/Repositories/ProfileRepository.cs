using Core.DTOs;
using Core.Interfaces;
using Storage.DataAccessLayer;
using Storage.Identity;
using Storage.Interfaces;
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

            var toReadBooksIdList = user.ReadStatuses.Where(rs => rs.IsWantRead == true).ToList();
            var favouriteBooksIdList = user.ReadStatuses.Where(rs => rs.IsFavorite == true).ToList();
            var areReadBooksIdList = user.ReadStatuses.Where(rs => rs.IsRead == true).ToList();

            UserProfile profile = new()
            {
                User = user,
                IsFriend = isFriend,
                FollowersCount = user.Followers.Count,
                FollowingsCount = user.FollowedUsers.Count + user.FollowedAuthors.Count,
                FavouriteBooks = isFriend || currentUserId == user.Id ? _context.Books.Where(b => toReadBooksIdList.Any(idlist => idlist.BookId == b.Id)) : null,
                ToReadBooks = _context.Books.Where(b => toReadBooksIdList.Any(idlist => idlist.BookId == b.Id)),
                AreReadBooks = _context.Books.Where(b => areReadBooksIdList.Any(idlist => idlist.BookId == b.Id)),
                //Photo = NOT IMPLEMENTED
            };

            return profile;
        }
    }
}
