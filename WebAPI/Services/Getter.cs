using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;
using WebAPI.DTOs;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class Getter<T> : IGetter<T> where T : class, IDbModel, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public Getter(IBaseRepository<T> postRepository, IMapper mapper)
        {
            _repository = postRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IResponseDto>> GetAll<IResponseDto>()
        {
            var responseModels = await _repository.FindAll();

            return _mapper.Map<IEnumerable<IResponseDto>>(responseModels);
        }

        public async Task<IResponseDto> GetById<IResponseDto>(int id)
        {
            var entity = await _repository.FindByConditions(x => x.Id == id);

            return _mapper.Map<IResponseDto>(entity.FirstOrDefault());
        }
    }
}
