using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Core.DTOs;

namespace Core.Interfaces
{
    public interface IUpdaterService<T> where T : IDbModel
    {
        public Task Update(IRequestDto updateModel, int id);
    }
}
