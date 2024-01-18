using Microsoft.AspNetCore.Mvc;
using skillsight.API.Data;
using skillsight.API.Models;

using static BCrypt.Net.BCrypt;

// Namespace grouping API controllers
namespace skillsight.API.Controllers;

// UserController handles user-related API requests
// Attribute 'ApiController' is used to denote a controller with API-specific functionalities
[ApiController]
// Attribute 'Route' defines the route template for this controller
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // Database context for data access
    private readonly ApplicationDbContext _context;

    // Constructor to inject the database context
    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userDTO)
    {
        var usernameExists = _context.Users.Any(u => u.Username == userDTO.Username);
        var emailExists = _context.Users.Any(u => u.Email == userDTO.Email);

        if (usernameExists || emailExists)
        {
            return BadRequest(new { message = "Username or Email already exists." });
        }


        var hashedPassword = HashPassword(userDTO.Password);
        var user = new User
        {
            Firstname = userDTO.Firstname,
            Lastname = userDTO.Lastname,
            Username = userDTO.Username,
            Email = userDTO.Email,
            Password = hashedPassword,
            JobRoleId = userDTO.JobRoleId
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Return success message as an HTTP 200 OK response
        return Ok(new { message = "User registered successfully" });
    }
}