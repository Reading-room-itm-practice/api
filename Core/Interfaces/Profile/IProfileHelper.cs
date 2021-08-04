using Core.DTOs;
using System;
using System.Threading.Tasks;

namespace Storage.Interfaces
{
    public interface IProfileHelper
    {
        public Task<UserProfile> GetUserProfile(Guid? id);
    }
}
