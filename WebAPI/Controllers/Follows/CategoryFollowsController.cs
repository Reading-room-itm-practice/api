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
    public class CategoryFollowsController : ControllerBase
    {
        private readonly IDeleterService<CategoryFollow> _deleterService;
        private readonly IExtendedGetterService<CategoryFollow> _getterService;
        private readonly ICreatorService<CategoryFollow> _creatorService;

        public CategoryFollowsController(IDeleterService<CategoryFollow> deleter, IExtendedGetterService<CategoryFollow> getter, ICreatorService<CategoryFollow> creator)
        {
            _deleterService = deleter;
            _getterService = getter;
            _creatorService = creator;
        }

        [SwaggerOperation(Summary = "Retrieves all categories follows")]
        [Route("api/users/{id:guid}/category-follows")]
        [HttpGet]
        public async Task<ServiceResponse> Index(Guid userId)
        {
            return await _getterService.GetAllByCreator<FollowDto>(userId);
        }

        [SwaggerOperation(Summary = "Create category follow for logged user")]
        [Route("api/categories/{id:int}/follows")]
        [HttpPost]
        public async Task<ServiceResponse> Create(int categoryId)
        {
            return await _creatorService.Create<FollowDto>(new FollowRequest { FollowableId = categoryId });
        }

        [SwaggerOperation(Summary = "Delete a category follow by unique id")]
        [Route("api/category-follows/{id:int}")]
        [HttpDelete]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _deleterService.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }
    }
}
