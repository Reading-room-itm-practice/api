﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Storage.Iterfaces;

namespace Core.Interfaces
{
    public interface ICrudService<T> : ICreatorService<T>, IGetterService<T>, IUpdaterService<T>, IDeleterService<T> where T : IDbModel
    {
        public Task<IReponseDto> Create<IReponseDto>(IRequest model);
        public Task<IEnumerable<IResponseDto>> GetAll<IResponseDto>();
        public Task<IResponseDto> GetById<IResponseDto>(int id);
        public Task Update(IRequest updateModel, int id);
        public Task Delete(int id);
    }
}
