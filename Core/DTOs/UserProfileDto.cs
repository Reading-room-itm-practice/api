using Storage.Identity;
using Storage.Models;
using Storage.Models.Photos;
using System.Collections.Generic;

namespace Core.DTOs
{
    public class UserProfileDto
    {
        public UserSearchDto User { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingsCount { get; set; }
        public PhotoDto Photo { get; set; }
        public IEnumerable<BookDto> FavouriteBooks { get; set; }
        public IEnumerable<BookDto> ToReadBooks { get; set; }
        public IEnumerable<BookDto> ReadBooks { get; set; }
        public IEnumerable<FriendDto> FriendList { get; set; }
    }

    public class UserProfile
    {
        public User User { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingsCount { get; set; }
        public ProfilePhoto Photo { get; set; }
        public PhotoDto Photo { get; set; }
        public IEnumerable<Book> FavouriteBooks { get; set; }
        public IEnumerable<Book> ToReadBooks { get; set; }
        public IEnumerable<Book> ReadBooks { get; set; }
        public IEnumerable<FriendDto> FriendList { get; set; }
    }

    public class ForeignUserProfile : UserProfile
    {
        public bool IsFriend { get; set; }
        public bool IsFollowing { get; set; }

        public ForeignUserProfile(UserProfile profile)
        {
            User = profile.User;
            FollowersCount = profile.FollowersCount;
            FollowingsCount = profile.FollowingsCount;
            Photo = profile.Photo;
            ToReadBooks = profile.ToReadBooks;
            ReadBooks = profile.ReadBooks;
            FriendList = profile.FriendList;
        }
    }

    public class ForeignUserProfileDto : UserProfileDto
    {
        public bool IsFriend { get; set; }
        public bool IsFollowing { get; set; }
    }
}
