using Core.DTOs;
using Storage.Identity;

namespace Core.Interfaces
{
    public interface IProfileRepository
    {
        public UserProfile GetProfile(User user, bool isFriend);
    }
}