using Microsoft.AspNetCore.Mvc;
using skillsight.API.Data;


namespace skillsight.API.Controllers;

// Controller for API endpoints
[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    // Database context
    private readonly ApplicationDbContext _context;

    // Constructor initializing the controller with database context
    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET method returning a welcome message
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Welcome to Skillsight APi");
    }
}
