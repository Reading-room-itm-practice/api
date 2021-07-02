using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces.Authors
{
    public interface IAuthorDeleter
    {
        public Task DeleteAuthor(int id);
    }
}
