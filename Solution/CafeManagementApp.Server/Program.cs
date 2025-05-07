using Bootstrap;
using CafeManagementApp.Server.Infrastructure.Filter;
using CafeManagementApp.Server.Infrastructure.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagementApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container.
            Startup.Init(builder.Host);
            
            //Add controllers with custom JSON options
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    //set json serializer to default casing
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("https://localhost:51989")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            //bypass default invalid filter setup
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            //use custom validation filter
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; // Default casing
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //Use CORS middleware
            app.UseCors("AllowSpecificOrigins");

            app.UseAuthorization();

            //Using custom middleware
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapControllers();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
