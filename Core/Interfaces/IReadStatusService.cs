using Core.DTOs;
using Core.Requests;
using Core.ServiceResponses;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReadStatusService
    {
        public Task Create(ReadStatusRequest request, int bookId, IEnumerable<ReadStatus> readStatus);
        public Task<ServiceResponse> Update(ReadStatusRequest request, int bookId);
        public Task<ServiceResponse<ReadStatusDto>> GetReadStatus(int bookId);
    }
}
