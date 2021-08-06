using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Follows;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Storage.Models.Likes;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers.Likes
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewLikesController : ControllerBase
    {
        private readonly IDeleterService<ReviewLike> _deleterService;
        private readonly IGetterService<ReviewLike> _getterService;
        private readonly ICreatorService<ReviewLike> _creatorService;

        public ReviewLikesController(IDeleterService<ReviewLike> deleter, IGetterService<ReviewLike> getter, ICreatorService<ReviewLike> creator)
        {
            _deleterService = deleter;
            _getterService = getter;
            _creatorService = creator;
        }


        [SwaggerOperation(Summary = "Create category follow for logged user")]
        [Route("reviews/{id:int}/likes")]
        [HttpPost]
        public async Task<ServiceResponse> Create(int id)
        {
            return await _creatorService.Create<FollowDto>(new LikeRequest());
        }


        [SwaggerOperation(Description = "Delete a follow by unique id")]
        [Route("reviews/likes/{id:int}")]
        [HttpDelete]
        public async Task<ServiceResponse> Delete(int id)
        {
            await _deleterService.Delete(id);

            return ServiceResponse.Success("Resource has been deleted");
        }
    }
}
