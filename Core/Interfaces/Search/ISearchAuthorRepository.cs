using Core.DTOs;
using Core.Enums;
using Core.Services;

namespace Core.Interfaces.Search
{
    interface ISearchAuthorRepository
    {
        DataDto<AuthorDto> GetAuthors(PaginationFilter filter, string searchString, SortType? sort);
    }
}
