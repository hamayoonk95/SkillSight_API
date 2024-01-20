using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using skillsight.API.Data;
using skillsight.API.Services;
using skillsight.API.Utilities;

namespace skillsight.API;
public class Program
{
    // Entry point of the application
    public static void Main(string[] args)
    {
        // Creating a builder for the web application
        var builder = WebApplication.CreateBuilder(args);


        // Bind JWT settings
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
        builder.Services.AddSingleton(jwtSettings);

        // Adding controller services to the container
        builder.Services.AddControllers();

        builder.Services.AddSingleton<TokenService>();
        builder.Services.AddScoped<AuthService>();
        // Add JWT auth
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            ClockSkew = TimeSpan.Zero
        };
    });

        // Configuring CORS (Cross-Origin Resource Sharing) to allow all origins, methods, and headers
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        // Creating and configuring the database connection string
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Adding DbContext to the services container with MySQL configuration
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // Building the web application
        var app = builder.Build();

        // Enabling CORS with the specified policy
        app.UseCors("AllowAll");

        // Enforces HTTPS redirection
        app.UseHttpsRedirection();

        // Adds authorization middleware
        app.UseAuthorization();
        app.UseAuthentication();

        // Maps controller endpoints
        app.MapControllers();

        // Runs the web application
        app.Run();
    }
}