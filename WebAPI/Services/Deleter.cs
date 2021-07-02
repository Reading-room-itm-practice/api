using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class Deleter<T> : IDeleter<T> where T : class, IDbModel, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;

        public Deleter(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task Delete(int id)
        {
            var entity = await _repository.FindByConditions(x => x.Id == id);
            await _repository.Delete(entity.FirstOrDefault());
        }
    }
}
