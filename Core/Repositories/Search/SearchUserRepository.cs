using AutoMapper;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Search;
using Core.Services;
using Storage.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;

namespace Core.Repositories.Search
{
    class SearchUserRepository : ISearchUserRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        public SearchUserRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DataDto<UserSearchDto> GetUsers(PaginationFilter filter, string searchString, SortType? sort)
        {
            var searchQueries = AdditionalSearchMethods.ProcessSearchString(searchString);
            var users = GetUsers(searchQueries);

            users = AdditionalSearchMethods.SortUsers(users, sort);
            return AdditionalSearchMethods.Pagination(filter, users);
        }

        private IEnumerable<UserSearchDto> GetUsers(string[] searchQueries)
        {
            var users = (_mapper.Map<IEnumerable<UserSearchDto>>(_context.Users))
                            .Where(u => AdditionalSearchMethods.ContainsQuery(u.UserName, searchQueries));
            return users;
        }
    }
}
