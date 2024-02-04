using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using skillsight.API.Utilities;

namespace skillsight.API.Services;

// TokenService - responsible for generating JWT (JSON Web Tokens) for authentication.
public class TokenService
{
    // Holds JWT settings like secret key, issuer, audience, etc.
    private readonly JwtSettings _jwtSettings;

    // Constructor injecting JwtSettings for token generation.
    public TokenService(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    // Generates an access token for a user based on their claims.
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        // Convert the secret key from JwtSettings into a byte array.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        // Create the credentials for signing the token with the security key
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Constructs the JWT token with specified parameters including issuer, audience, claims, expiration time, and signing credentials.
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            signingCredentials: creds);

        // Creates a string representation of the JWT token and returns it.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
