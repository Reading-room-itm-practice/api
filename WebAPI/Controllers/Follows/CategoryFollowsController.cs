using Core.DTOs.Follows;
using Core.Interfaces;
using Core.Requests.Follows;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Storage.Models.Follows;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Follows
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryFollowsController : ControllerBase
    {
        private readonly ICrudService<CategoryFollow> _followsService;

        public CategoryFollowsController(ICrudService<CategoryFollow> crudService)
        {
            _followsService = crudService;
        }

        [SwaggerOperation(Summary = "Retrieves all categories follows")]
        [HttpGet]
        public async Task<ServiceResponse> Index()
        {
            return await _followsService.GetAll<FollowDto>();
        }

        [SwaggerOperation(Summary = "Create category follow for logged user")]
        [HttpPost]
        public async Task<ServiceResponse> Create(int categoryId)
        {
            return await _followsService.Create<FollowDto>(new FollowRequest { FollowableId = categoryId });
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
