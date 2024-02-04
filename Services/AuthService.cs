using skillsight.API.Data;
using System.Security.Claims;
using skillsight.API.DTOs;
using static BCrypt.Net.BCrypt;

namespace skillsight.API.Services;

// AuthService handles authentication related functionalities.
public class AuthService
{
    // Database context for accessing user data.
    private readonly ApplicationDbContext _context;
    // TokenService for handling JWT token generation.
    private readonly TokenService _tokenService;

    // Constructor injecting ApplicationDbContext and TokenService.
    public AuthService(ApplicationDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    // Method to authenticate a user with username and password.
    public UserTokenDTO? Authenticate(string username, string password)
    {
        // // Fetch the user from the database based on the username
        var user = _context.Users.SingleOrDefault(u => u.Username == username);

        // Validate the user's password. If user is null or password verification fails, return null.
        if (user == null || !Verify(password, user.Password))
        {
            return null; // Returning null indicates invalid credentials.
        }

        // Prepare claims for the token.
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

        // Generate an access token using the TokenService.
        var accessToken = _tokenService.GenerateAccessToken(claims);

        // Return the generated access token encapsulated in UserTokenDTO.
        return new UserTokenDTO
        {
            AccessToken = accessToken,
        };
    }
}
