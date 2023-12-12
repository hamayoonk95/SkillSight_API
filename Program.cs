using Microsoft.EntityFrameworkCore;
using skillsight.API.Data;

namespace skillsight.API;
public class Program
{
    // Entry point of the application
    public static void Main(string[] args)
    {
        // Creating a builder for the web application
        var builder = WebApplication.CreateBuilder(args);

        // Adding controller services to the container
        builder.Services.AddControllers();

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

        // Retrieving database credentials from configuration
        var userId = builder.Configuration["DatabaseCredentials:UserId"];
        var password = builder.Configuration["DatabaseCredentials:Password"];

        // Creating and configuring the database connection string
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            .Replace("USERID", userId)
            .Replace("PASSWORD", password);

        // Adding DbContext to the services container with MySQL configuration
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // Building the web application
        var app = builder.Build();

        // Enforces HTTPS redirection
        app.UseHttpsRedirection();

        // Adds authorization middleware
        app.UseAuthorization();

        // Enabling CORS with the specified policy
        app.UseCors("AllowAll");

        // Maps controller endpoints
        app.MapControllers();

        // Runs the web application
        app.Run();
    }
}