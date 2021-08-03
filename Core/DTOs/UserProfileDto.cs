using Storage.Identity;
using Storage.Models;
using System.Collections.Generic;

namespace Core.DTOs
{
    public class UserProfileDto
    {

        public UserSearchDto User;
        public int FollowersCount;
        public int FollowingsCount;
        public PhotoDto Photo;
        public IEnumerable<BookDto> FavouriteBooks;
        public IEnumerable<BookDto> ToReadBooks;
        public IEnumerable<BookDto> AreReadBooks;
        public IEnumerable<UserSearchDto> FriendList;
        public bool IsFriend;
    }

    public class UserProfile
    {

        public User User;
        public int FollowersCount;
        public int FollowingsCount;
        public PhotoDto Photo;
        public IEnumerable<Book> FavouriteBooks;
        public IEnumerable<Book> ToReadBooks;
        public IEnumerable<Book> AreReadBooks;
        public IEnumerable<User> FriendList;
        public bool IsFriend;
    }
}
