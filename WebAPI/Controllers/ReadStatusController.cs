using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{bookId:int}/readStatus")]
        public async Task<ServiceResponse<ReadStatusDto>> Get(int bookId)
        {
            return await _readStatusGetterService.GetReadStatus(bookId);
        }

        [HttpPut("Update/{bookId:int}/readStatus")]
        public async Task<ServiceResponse> Update(ReadStatusRequest request, int bookId)
        {
            request.BookId = bookId;
            return await _readStatusUpdaterService.UpdateReadStatus(request);
        }
    }
}
