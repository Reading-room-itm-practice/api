using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Core.Exceptions;
using Storage.Iterfaces;
using Core.Interfaces;

namespace Core.Services
{
    public class CrudService<T> : ICrudService<T> where T : class, IDbModel, IDbMasterKey
    {
        private readonly ICreatorService<T> Creator;
        private readonly IGetterService<T> Getter;
        private readonly IUpdaterService<T> Updater;
        private readonly IDeleterService<T> Deleter;

        public CrudService(ICreatorService<T> creator, IGetterService<T> getter, IUpdaterService<T> updater, IDeleterService<T> deleter)
        {
            Creator = creator;
            Getter = getter;
            Updater = updater;
            Deleter = deleter;
        }

        public async Task<IResponseDto> Create<IResponseDto>(IRequestDto model)
        {
            return await Creator.Create<IResponseDto>(model);
        }

        public async Task<IEnumerable<IResponseDto>> GetAll<IResponseDto>()
        {
            return await Getter.GetAll<IResponseDto>();
        }

        public async Task<IResponseDto> GetById<IResponseDto>(int id)
        {
            return await Getter.GetById<IResponseDto>(id);
        }

        public async Task Update(IRequestDto updateModel, int id)
        {
            await Updater.Update(updateModel, id);
        }

        public async Task Delete(int id)
        {
            await Deleter.Delete(id);
        }
    }
}
