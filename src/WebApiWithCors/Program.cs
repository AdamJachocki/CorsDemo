using Microsoft.AspNetCore.Cors.Infrastructure;
using WebApiWithCors.Services.Cors;

namespace WebApiWithCors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(setup =>
            {
                setup.AddPolicy("get-policy", setup =>
                {
                    setup.WithOrigins("http://localhost:5001")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

                setup.AddPolicy("set-policy", setup =>
                {
                    setup.WithOrigins("http://localhost:5001")
                    .WithMethods("POST")
                    .WithHeaders("Content-Type");
                });

                setup.AddPolicy("http://localhost:5001", setup =>
                {
                    setup.WithOrigins("http://localhost:5001")
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST");
                });

                setup.AddPolicy("http://localhost:5011", setup =>
                {
                    setup.WithOrigins("http://localhost:5011")
                    .WithMethods("DELETE")
                    .WithHeaders();
                    
                });
            });

            builder.Services.AddTransient<ICorsPolicyProvider, OriginCorsPolicyProvider>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}