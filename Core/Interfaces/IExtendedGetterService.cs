using Core.ServiceResponses;
using Storage.Interfaces;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IExtendedGetterService<T> : IGetterService<T> where T : AuditableModel, IDbModel
    {
        public Task<ServiceResponse<IEnumerable<IDto>>> GetAllByCreator<IDto>(Guid userId);
    }
}
