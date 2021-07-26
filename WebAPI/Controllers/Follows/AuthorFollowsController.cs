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
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthorFollowsController : ControllerBase
    {
        private readonly ICrudService<AuthorFollow> _crudService;

        public AuthorFollowsController(ICrudService<AuthorFollow> crudService)
        {
            _crudService = crudService;
        }

        [SwaggerOperation(Summary = "Retrieves all authors follows")]
        [HttpGet]
        public async Task<ServiceResponse> Index()
        {
            return await _crudService.GetAll<FollowDto>();
        }

        [SwaggerOperation(Summary = "Create author follow for logged user")]
        [HttpPost]
        public async Task<ServiceResponse> Create(int authorId)
        {
            return await _crudService.Create<FollowDto>(new FollowRequest {FollowableId = authorId });
        }

        [SwaggerOperation(Summary = "Delete a follow by unique id")]
        [HttpDelete("{id:int}")]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _crudService.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }

    }
}
