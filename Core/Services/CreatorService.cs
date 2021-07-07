using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Core.Interfaces;
using Storage.Iterfaces;

namespace Core.Services
{
    public class CreatorService<T> : ICreatorService<T> where T : class, IDbModel
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public CreatorService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IResponseDto> Create<IResponseDto>(IRequestDto requestDto)
        {
            var model = _mapper.Map<T>(requestDto);
            await _repository.Create(model);
           
            return _mapper.Map<IResponseDto>(model);
        }
    }
}
