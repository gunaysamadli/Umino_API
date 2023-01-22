using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using UminoWeb.BLL;
using UminoWeb.BLL.Mapping;
using UminoWeb.DAL;
using UminoWeb.DAL.DataContext;

namespace UminoWeb.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            var MyAllowSpecificOrigins = "Access-Control-Allow-Origin";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000", "http://localhost:3001").AllowAnyHeader().AllowAnyMethod().AllowCredentials(); ;
                                  });
            });

            builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(assembly: Assembly.GetExecutingAssembly()));


            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddDalServices();
            builder.Services.AddBllServices();

            var app = builder.Build();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "images")),
                RequestPath = new PathString("/images"),
                EnableDirectoryBrowsing = true
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("Access-Control-Allow-Origin");


            app.MapControllers();

            app.Run();
        }
    }
}