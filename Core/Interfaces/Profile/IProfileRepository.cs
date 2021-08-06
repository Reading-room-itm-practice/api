using Core.DTOs;
using Storage.Identity;
using Storage.Models;
using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IProfileRepository
    {
        public UserProfile GetProfile(User user, bool isFriend);
        public UserProfile BaseProfile(User user, IEnumerable<Book> toReadBooks, IEnumerable<Book> readingBooks);
        public ForeignUserProfile CreateForeignProfile(UserProfile profile, User user, bool isFriend, Guid currentUserId, List<Book> favouriteBooks);
        public UserProfile CreateMyProfile(UserProfile profile, IEnumerable<Book> favouriteBooks);
    }
}