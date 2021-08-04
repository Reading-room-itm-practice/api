using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadStatusController : ControllerBase
    {
        private readonly IReadStatusService _readStatusService;

        public ReadStatusController(IReadStatusService readStatusService)
        {
            _readStatusService = readStatusService;
        }

        [HttpPut("Update/{bookId}")]
        public async Task<ServiceResponse> Update(ReadStatusRequest request, int bookId)
        {
            return await _readStatusService.Update(request, bookId);
        }

        [HttpGet("{bookId}")]
        public async Task<ServiceResponse<ReadStatusDto>> Get(int bookId)
        {
            return await _readStatusService.GetReadStatus(bookId);
        }
    }
}
