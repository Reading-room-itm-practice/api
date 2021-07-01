using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Interfaces.Authors;

namespace WebAPI.Services.Authors
{
    public class AuthorGetter : IAuthorGetter
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorGetter(IAuthorRepository postRepository, IMapper mapper)
        {
            _repository = postRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthors()
        {
            var posts = await _repository.GetAll();
            return _mapper.Map<IEnumerable<AuthorDto>>(posts);
        }

        public async Task<AuthorDto> GetAuthorById(int id)
        {
            var post = await _repository.GetById(id);
            return _mapper.Map<AuthorDto>(post);
        }
    }
}
