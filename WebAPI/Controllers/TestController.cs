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
    public class TestController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public TestController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpPost()]
        public  ActionResult<Category> Create(string value)
        {
            _context.Categories.Add(new Category { Name = value });
            _context.SaveChanges();

            return Ok("Resource created");
        }

        [HttpGet]
        public IEnumerable<Category> Index()
        {
            return _context.Categories.ToArray();
        }
    }
}
