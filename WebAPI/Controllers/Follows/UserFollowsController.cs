using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Storage.Models.Follows;

namespace WebAPI.Controllers.Follows
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFollowsController : ControllerBase
    {
        private readonly ICrudService<UserFollow> _crudService;
    }
}
