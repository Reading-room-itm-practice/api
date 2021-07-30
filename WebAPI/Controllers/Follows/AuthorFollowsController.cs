using Core.DTOs.Follows;
using Core.Interfaces;
using Core.Requests.Follows;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.Models.Follows;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Follows
{
    [ApiController]
    public class AuthorFollowsController : ControllerBase
    {
        private readonly IDeleterService<AuthorFollow> _deleterService;
        private readonly IExtendedGetterService<AuthorFollow> _getterService;
        private readonly ICreatorService<AuthorFollow> _creatorService;

        public AuthorFollowsController(IDeleterService<AuthorFollow> deleter, IExtendedGetterService<AuthorFollow> getter, ICreatorService<AuthorFollow> creator)
        {
            _deleterService = deleter;
            _getterService = getter;
            _creatorService = creator;
        }

        [SwaggerOperation(Description = "Retrieves all authors follows")]
        [Route("api/users/{id:guid}/author-follows")]
        [HttpGet]
        public async Task<ServiceResponse> Index(Guid id)
        {
            return await _getterService.GetAllByCreator<FollowDto>(id);
        }

        [SwaggerOperation(Description  = "Create author follow for logged user")]
        [Route("api/authors/{id:int}/follows")]
        [HttpPost]
        public async Task<ServiceResponse> Create(int id)
        {
            return await _creatorService.Create<FollowDto>(
                new FollowRequest {FollowableId = id , CreatorId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)) });
        }

        [SwaggerOperation(Description = "Delete a follow by unique id")]
        [Route("api/author-follows/{id:int}")]
        [HttpDelete]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _deleterService.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }
    }
}
