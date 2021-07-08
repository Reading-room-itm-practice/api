using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Core.Exceptions;
using WebAPI.Interfaces;
using Storage.Iterfaces;
using Core.Interfaces;

namespace WebAPI.Services
{
    public class CrudService<T> : ICrudService<T> where T : class, IDbModel, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public CrudService(IBaseRepository<T> repository, IMapper mapper)
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

        public async Task<IEnumerable<IResponseDto>> GetAll<IResponseDto>()
        {
            var models = await _repository.FindAll();

            return _mapper.Map<IEnumerable<IResponseDto>>(models);
        }

        public async Task<IResponseDto> GetById<IResponseDto>(int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);

            return _mapper.Map<IResponseDto>(model.FirstOrDefault());
        }

        public async Task Update(IRequestDto requestDto, int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);

            if (model.FirstOrDefault() == null)
            {
                throw new NotFoundException("Entity does not exists");
            }

            var updatedModel = _mapper.Map(requestDto, model.FirstOrDefault());
            await _repository.Edit(updatedModel);
        }

        public async Task Delete(int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);

            if (model.FirstOrDefault() == null)
            {
                throw new NotFoundException("Entity does not exists");
            }

            await _repository.Delete(model.FirstOrDefault());
        }
    }
}
