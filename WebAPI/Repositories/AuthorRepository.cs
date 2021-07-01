using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DataAccessLayer;
using WebAPI.Interfaces.Authors;
using WebAPI.Models;

namespace WebAPI.Repositories
{

    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(ApiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetById(int id)
        {
            return  await _context.Authors.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
