using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using skillsight.API.Utilities;

namespace skillsight.API.Services
{
    public class TokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            // Create the security key with the secret key from JwtSettings
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            // Create the credentials for signing the token with the security key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define the token - consist of issuer, audience, claims, expiration, and the signing credentials
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: creds);

            // Create a token string from the token definition and return it
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Method for generating a refresh token can be added here.
        // Method for validating a token can be added here.
    }
}
