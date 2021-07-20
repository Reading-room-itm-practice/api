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
        public new Task<IReponseDto> Create<IReponseDto>(IRequest model);
        public new Task<IEnumerable<IResponseDto>> GetAll<IResponseDto>();
        public new Task<IResponseDto> GetById<IResponseDto>(int id);
        public new Task Update(IRequest updateModel, int id);
        public new Task Delete(int id);
    }
}
