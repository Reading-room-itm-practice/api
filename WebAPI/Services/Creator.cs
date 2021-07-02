using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class Creator<T> : ICreator<T> where T : class, IDbModel
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public Creator(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IResponseDto> Create<IResponseDto>(IRequestDto requestModel)
        {
            var entityToAdd = _mapper.Map<T>(requestModel);
            await _repository.Create(entityToAdd);
           
            return _mapper.Map<IResponseDto>(entityToAdd);
        }
    }
}
