using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;
using WebAPI.Exceptions;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class DeleterService<T> : IDeleterService<T> where T : class, IDbModel, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;

        public DeleterService(IBaseRepository<T> repository)
        {
            _repository = repository;
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
