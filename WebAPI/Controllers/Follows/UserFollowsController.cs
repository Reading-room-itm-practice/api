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
    [ApiController]
    public class UserFollowsController : ControllerBase
    {
        private readonly IDeleterService<UserFollow> _deleterService;
        private readonly IExtendedGetterService<UserFollow> _getterService;
        private readonly ICreatorService<UserFollow> _creatorService;

        public UserFollowsController(IDeleterService<UserFollow> deleter, IExtendedGetterService<UserFollow> getter, ICreatorService<UserFollow> creator)
        {
            _deleterService = deleter;
            _getterService = getter;
            _creatorService = creator;
        }

        [SwaggerOperation(Summary = "Retrieves all categories follows")]
        [Route("api/users/{id:guid}/user-follows")]
        [HttpGet]
        public async Task<ServiceResponse> Index(Guid userId)
        {
            return await _getterService.GetAllByCreator<UserFollowDto>(userId);
        }

        [SwaggerOperation(Summary = "Create category follow for logged user")]
        [Route("api/users/{id:guid}/follows")]
        [HttpPost]
        public async Task<ServiceResponse> Create(Guid userId)
        {
            return await _creatorService.Create<UserFollowDto>(new UserFollowRequest { FollowableId = userId });
        }

        [SwaggerOperation(Summary = "Delete a category follow by unique id")]
        [Route("api/user-follows/{id:int}")]
        [HttpDelete]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _deleterService.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }
    }
}
