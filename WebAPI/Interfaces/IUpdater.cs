using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;
using WebAPI.DTOs;

namespace WebAPI.Interfaces
{
    public interface IUpdater<T> where T : IDbModel
    {
        public Task UpdateAuthor(IRequestDto updateModel, int id);
    }
}
