using Microsoft.AspNetCore.Mvc;
using skillsight.API.Data;
using skillsight.API.Models;
using skillsight.API.Services;
// using skillsight.API.DTOs;
using static BCrypt.Net.BCrypt;

// Namespace grouping API controllers
namespace skillsight.API.Controllers;

/*********
    UsersController handles user-related API requests.
    Attribute 'ApiController' is used to denote a controller with API-specific functionalities.
    Attribute 'Route' defines the route template for this controller.
**********/
[ApiController]
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

    /******** 
        Registers a new user with the provided details.
        Endpoint/Route: POST api/users/register
    ********/
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userDTO)
    {
        // Check if username and email exists in database, return error if exists
        var usernameExists = _context.Users.Any(u => u.Username == userDTO.Username);
        var emailExists = _context.Users.Any(u => u.Email == userDTO.Email);

        if (usernameExists)
            return BadRequest(new { message = "Username already exists." });
        if (emailExists)
            return BadRequest(new { message = "Email already exists." });

        // Hash the password
        var hashedPassword = HashPassword(userDTO.Password);

        // Create a new User with the information from FormBody
        var user = new User
        {
            Firstname = userDTO.Firstname,
            Lastname = userDTO.Lastname,
            Username = userDTO.Username,
            Email = userDTO.Email,
            Password = hashedPassword,
            JobRoleId = userDTO.JobRoleId
        };

        // Add the User to the Database
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Return success message as an HTTP 200 OK response
        return Ok(new { message = "User registered successfully" });
    }

    /******** 
        Authenticates a user and generates a token.
        Endpoint/Route: POST api/users/login
    ********/
    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] UserLoginDTO userDTO)
    {
        // Authenticate the user using AuthService
        var tokenDto = _authService.Authenticate(userDTO.Username, userDTO.Password);
        // If token is null return error
        if (tokenDto == null)
        {
            return BadRequest(new { message = "Wrong Username or Password." });
        }
        // return JWT access token and success message
        return Ok(new { message = "Logged In Successfully!", token = tokenDto.AccessToken });
    }
}