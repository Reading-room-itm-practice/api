using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DataAccessLayer;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PostController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpPost()]
        public  ActionResult<TestModel> Create(string value)
        {
            _context.TestModels.Add(new TestModel { TestValue = value });
            _context.SaveChanges();

            return Ok("Resource created");
        }

        [HttpGet]
        public IEnumerable<TestModel> Index()
        {
            return _context.TestModels.ToArray();
        }
    }
}
