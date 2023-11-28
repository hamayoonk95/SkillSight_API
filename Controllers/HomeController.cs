using Microsoft.AspNetCore.Mvc;
using skillsight.API.Data;


namespace skillsight.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to Skillsight APi");
        }
    }
}