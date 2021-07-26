using Core.DTOs.Follows;
using Core.Interfaces;
using Core.Requests.Follows;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Storage.Models.Follows;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Follows
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFollowsController : ControllerBase
    {
        private readonly ICrudService<UserFollow> _followsService;
        public UserFollowsController(ICrudService<UserFollow> crudService)
        {
            _followsService = crudService;
        }

        [SwaggerOperation(Summary = "Retrieves all categories follows")]
        [HttpGet]
        public async Task<ServiceResponse> Index()
        {
            return await _followsService.GetAll<UserFollowDto>();
        }

        [SwaggerOperation(Summary = "Create category follow for logged user")]
        [HttpPost]
        public async Task<ServiceResponse> Create(Guid userId)
        {
            return await _followsService.Create<UserFollowDto>(new UserFollowRequest { FollowableId = userId });
        }

        [SwaggerOperation(Summary = "Delete a category follow by unique id")]
        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _followsService.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }
    }
}
