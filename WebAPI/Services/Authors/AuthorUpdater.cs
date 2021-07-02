using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Interfaces;
using WebAPI.Interfaces.Authors;

namespace WebAPI.Services.Authors
{
    public class AuthorUpdater : IAuthorUpdater
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;


        public AuthorUpdater(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task UpdateAuthor(UpdateAuthorDto updateModel, int id)
        {
            var author = await _repository.GetById(id);
            var responseAuthor = _mapper.Map(updateModel, author);
            await _repository.Edit(responseAuthor);
        }
    }
}
