using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace WebAPI.Interfaces.Authors
{
    public interface IAuthorGetter
    {
        public Task<IEnumerable<AuthorDto>> GetAllAuthors();
        public Task<AuthorDto> GetAuthorById(int id);
    }
}
