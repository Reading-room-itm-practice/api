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
    [Route("api")]
    [ApiController]
    public class ReadStatusController : ControllerBase
    {
        private readonly IReadStatusUpdaterService _readStatusUpdaterService;
        private readonly IReadStatusGetterService _readStatusGetterService;

        public ReadStatusController(IReadStatusUpdaterService readStatusUpdaterService, IReadStatusGetterService readStatusGetterService)
        {
            _readStatusUpdaterService = readStatusUpdaterService;
            _readStatusGetterService = readStatusGetterService;
        }

        [HttpPut]
        [Route("books/{bookId:int}/read-status")]
        public async Task<ServiceResponse> Update(ReadStatusRequest request, int bookId)
        {
            request.BookId = bookId;
            return await _readStatusUpdaterService.UpdateReadStatus(request);
        }

        [HttpGet]
        [Route("books/{bookId:int}/read-status")]
        public async Task<ServiceResponse<ReadStatusDto>> Get(int bookId)
        {
            return await _readStatusGetterService.GetReadStatus(bookId);
        }
    }
}
