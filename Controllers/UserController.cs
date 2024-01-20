using Microsoft.AspNetCore.Mvc;
using skillsight.API.Data;
using skillsight.API.Models;
using skillsight.API.Services;
using skillsight.API.DTOs;

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
    private readonly AuthService _authService;

    // Constructor to inject the database context
    public UsersController(ApplicationDbContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userDTO)
    {
        var usernameExists = _context.Users.Any(u => u.Username == userDTO.Username);
        var emailExists = _context.Users.Any(u => u.Email == userDTO.Email);

        if (usernameExists)
        {
            return BadRequest(new { message = "Username already exists." });
        }

        if (emailExists)
        {
            return BadRequest(new { message = "Email already exists." });
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

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO userDTO)
    {
        // var user = _context.Users.FirstOrDefault(u => u.Username == userDTO.Username);
        // if (user == null)
        // {
        //     return BadRequest(new { message = "Wrong Username or Password." });
        // }
        var tokenDto = _authService.Authenticate(userDTO.Username, userDTO.Password);
        if (tokenDto == null)
        {
            return BadRequest(new { message = "Wrong Username or Password." });
        }

        return Ok(new { message = "Logged In Successfully!", token = tokenDto.AccessToken });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // Implement Logic
        return Ok(new { message = "Logged out successfully!" });
    }

}