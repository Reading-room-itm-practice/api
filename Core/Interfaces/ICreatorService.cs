using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Storage.Iterfaces;

namespace Core.Interfaces
{
    public interface ICreatorService<T> where T : IDbModel
    {
        public Task<IReponseDto> Create<IReponseDto>(IRequest model);
    }
}
