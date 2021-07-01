using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace WebAPI.Interfaces.Authors
{
    public interface IAuthorCreator
    {
        public Task<AuthorDto> AddAuthor(CreateAuthorDto model);
    }
}
