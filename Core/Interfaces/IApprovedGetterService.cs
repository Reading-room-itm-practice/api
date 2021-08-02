using Core.ServiceResponses;
using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IApprovedGetterService<T> where T : class, IApproveable, IDbMasterKey
    {
        public Task<ServiceResponse<IEnumerable<IDto>>> GetAllApproved<IDto>();
        public Task<ServiceResponse<IDto>> GetApprovedById<IDto>(int id);
    }
}
