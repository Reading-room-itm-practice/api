using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Interfaces;
using WebAPI.Interfaces.Authors;
using WebAPI.Models;

namespace WebAPI.Services.Authors
{
    public class AuthorCreator : IAuthorCreator
    {
        private readonly IBaseRepository _repository;
        private readonly IMapper _mapper;

        public AuthorCreator(IBaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AuthorDto> AddAuthor(CreateAuthorDto model)
        {
            var post = _mapper.Map<Author>(model);
            await _repository.Create(post);
            return _mapper.Map<AuthorDto>(post);
        }
    }
}
