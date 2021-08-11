using Core.Response;
using Storage.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IApprovedGetterService<T> where T : class, IApproveable, IDbMasterKey
    {
        public Task<ServiceResponse<IEnumerable<IDto>>> GetAllApproved<IDto>();
        public Task<ServiceResponse<IDto>> GetApprovedById<IDto>(int id);
    }
}
