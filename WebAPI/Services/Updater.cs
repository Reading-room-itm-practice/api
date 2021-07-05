using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class Updater<T> : IUpdater<T> where T : class, IDbModel, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public Updater(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Update(IRequestDto requestDto, int id)
        {
            var model = await _repository.FindByConditions( x => x.Id == id);
            var updatedModel = _mapper.Map(requestDto, model.FirstOrDefault());
            await _repository.Edit(updatedModel);
        }
    }
}
