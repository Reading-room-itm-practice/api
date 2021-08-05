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
    public interface IReadStatusUpdaterService
    {
        public Task<ServiceResponse> UpdateReadStatus(ReadStatusRequest request);
    }
}
