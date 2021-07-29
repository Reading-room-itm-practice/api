using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Search;
using Core.Services;
using Storage.DataAccessLayer;
using Storage.Identity;
using System.Linq;

namespace Core.Repositories.Search
{
    class SearchUserRepository : ISearchUserRepository
    {
        private readonly ApiDbContext _context;

        public SearchUserRepository(ApiDbContext context)
        {
            _context = context;
        }

        public DataDto<User> GetUsers(PaginationFilter filter, string searchString, SortType? sort)
        {
            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);
            var users = _context.Set<User>().AsEnumerable().
                Where(u => AdditionalSearchMethods.ContainsQuery(u.UserName, searchQueries)).AsQueryable();

            users = AdditionalSearchMethods.SortUsers(users, sort);

            return AdditionalSearchMethods.Pagination(filter, users.ToList().AsEnumerable());
        }
    }
}
