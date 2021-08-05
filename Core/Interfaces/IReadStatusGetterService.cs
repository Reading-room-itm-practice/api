using Core.DTOs;
using Core.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReadStatusGetterService
    {
        public Task<ServiceResponse<ReadStatusDto>> GetReadStatus(int bookId);
    }
}
