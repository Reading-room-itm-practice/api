using Core.DTOs;
using Core.DTOs.Follows;
using Core.Interfaces;
using Core.Requests;
using Core.Requests.Follows;
using Microsoft.AspNetCore.Mvc;
using Storage.Interfaces;
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

        //[SwaggerOperation(Summary = "Retrieves all book authors")]
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var authors = await _crud.GetAll<AuthorDto>();

        //    return Ok(authors);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(int authorId)
        {
            await _crudService.Create<FollowDto>(new FollowRequest {FollowableId = authorId });

            return Ok("Successfully following author");
        }

        [SwaggerOperation(Summary = "Delete a follow by unique id")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _crudService.Delete(id);

            return Ok("Resource deleted");
        }

    }
}
