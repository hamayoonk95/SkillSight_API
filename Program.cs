using Microsoft.EntityFrameworkCore;
using skillsight.API.Data;

namespace skillsight.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var userId = builder.Configuration["DatabaseCredentials:UserId"];
            var password = builder.Configuration["DatabaseCredentials:Password"];

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                .Replace("USERID", userId)
                .Replace("PASSWORD", password);

            // builder.Services.AddDbContext<ApplicationDbContext>(options =>
            // options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
            // new MySqlServerVersion(new Version(8, 0, 27))));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}