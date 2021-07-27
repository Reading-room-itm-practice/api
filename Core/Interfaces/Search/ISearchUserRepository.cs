using Core.DTOs;
using Core.Enums;
using Core.Services;

namespace Core.Interfaces.Search
{
    interface ISearchUserRepository
    {
        DataDto<UserSearchDto> GetUsers(PaginationFilter filter, string searchString, SortType? sort);
    }
}
