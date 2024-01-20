using skillsight.API.Data;
using System.Security.Claims;
using skillsight.API.DTOs;
using static BCrypt.Net.BCrypt;

namespace skillsight.API.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;
        // Inject other services like UserService to interact with the user data.

        public AuthService(ApplicationDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public UserTokenDTO Authenticate(string username, string password)
        {
            // Validate user credentials
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null || !Verify(password, user.Password))
            {
                return null; // or handle invalid credentials appropriately
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            // var refreshToken = _tokenService.GenerateRefreshToken(); 

            return new UserTokenDTO
            {
                AccessToken = accessToken,
                // RefreshToken = refreshToken
            };
        }

        // Method to handle refreshing tokens can be added here.
    }
}
