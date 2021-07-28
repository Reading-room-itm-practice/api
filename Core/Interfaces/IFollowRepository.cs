using Storage.Models.Follows;

namespace Core.Interfaces
{
    public interface IFollowRepository<T> : IBaseRepository<T> where T : Follow
    {
    }
}
