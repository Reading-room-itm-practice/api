using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces.Authors;
using WebAPI.Models;

namespace WebAPI.Services.Authors
{
    public class AuthorDeleter : IAuthorDeleter
    {
        private readonly IAuthorRepository _repository;

        public AuthorDeleter(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteAuthor(int id)
        {
            var author = await _repository.GetById(id);
            await _repository.Delete(author);
        }
    }
}
