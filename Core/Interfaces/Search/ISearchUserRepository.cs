using Core.DTOs;
using Core.Enums;
using Core.Services;
using Storage.Identity;

namespace Core.Interfaces.Search
{
    interface ISearchUserRepository
    {
        DataDto<User> GetUsers(PaginationFilter filter, string searchString, SortType? sort);
    }
}
